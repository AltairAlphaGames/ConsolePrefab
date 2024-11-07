using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

using TMPro;

public class ArgusApp : MonoBehaviour
{
    public Button close;  // The ListItem prefab
    public float typingSpeed = 0.05f;
    public GameObject argusResponsePrefab;  // The ListItem prefab
    public ScrollRect argusScrollRect;     // The Content panel in the Scroll View
    public Transform argusContentPanel;     // The Content panel in the Scroll View
    public TMP_Text argusText;     // The Content panel in the Scroll View

    private string ownerCharacter = "Unknown";
    private TextAsset argusFile;
    private ArgusData argusData;

    // Start is called before the first frame update
    void Start()
    {
        if (this.close != null)
        {
            this.close.onClick.AddListener(OnCloseClick);
        }    
    }

    void OnCloseClick()
    {
        this.gameObject.SetActive(false);
    }

    public void ClearText()
    {
        argusText.text = "";
    }

    public void SetActive(bool setActive)
    {
        this.gameObject.SetActive(setActive);
    }

    public void SetTerminalOwner( string o )
    {
        this.ownerCharacter = o;
    }

    public void ProcessArgusFile( TextAsset f )
    {
        this.argusFile = f;

        this.argusData = this.ParseArgusJson();
    }

    public ArgusData ParseArgusJson()
    {
        return JsonUtility.FromJson<ArgusData>(argusFile.text); 
    }

    public void ClearScrollView(Transform contentPanel)
    {
        // Loop through all the children of the content panel and destroy them
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }
    }

    public void executeArgusGreeting(int encounterIdx = 0, int conversationIdx = 0)
    {
        ClearScrollView(argusContentPanel);
        
        StartCoroutine(TypeText(encounterIdx, conversationIdx, argusData.encounters[encounterIdx].conversations[conversationIdx].text.Replace("[TerminalOwner]", this.ownerCharacter )));
    }

    IEnumerator TypeText(int encounterIdx = 0, int conversationIdx = 0, string typeText ="")
    {
        foreach (char letter in typeText)
        {
            if (this.gameObject.activeSelf != true)
            {
                break;
            }

            argusText.text += letter;
            argusScrollRect.normalizedPosition = new Vector2(0, 0);
            yield return new WaitForSeconds(typingSpeed);
        }

        argusText.text += "\n\n";

        populateResponseList(encounterIdx, conversationIdx);
    }

    void populateResponseList(int encounterIdx, int conversationIdx)
    {
        List<Response> commonMenu = argusData.encounters[encounterIdx].commonMenu;
        List<Response> responses = argusData.encounters[encounterIdx].conversations[conversationIdx].responses;
        
        List<Response> menuItems = commonMenu;
        if (responses.Count > 0)
        {
            menuItems = responses;
        }

        foreach (Response item in menuItems)
        {
            if (item.selectId != conversationIdx)
            {
                GameObject listItem = Instantiate(argusResponsePrefab, argusContentPanel); 
                Button btn = listItem.GetComponent<Button>();
                btn.onClick.AddListener(() => executeArgusGreeting(encounterIdx, item.selectId));
                TMP_Text[] textFields = listItem.GetComponentsInChildren<TMP_Text>(); 
                
                textFields[0].text = item.response.Replace("[TerminalOwner]", ownerCharacter);
            }
        }
    }

}


//Parse argus responses
[System.Serializable]
public class ArgusData
{
    public List<Encounters> encounters;

    public ArgusData(List<Encounters> encounters)
    {
        this.encounters = encounters;
    } 
}

[System.Serializable]
public class Encounters
{
    public int encounterId;
    public List<Response> commonMenu;
    public List<Conversations> conversations;

    public Encounters(int encounterId, List<Response> commonMenu, List<Conversations> conversations)
    {
        this.encounterId = encounterId;
        this.conversations = conversations;
        this.commonMenu = commonMenu;
    }
}

[System.Serializable]
public class Conversations
{
    public string text;
    public List<Response> responses;

    public Conversations(string text, List<Response> response)
    {
        this.text = text;
        this.responses = response;
    }
}

[System.Serializable]
public class Response
{
    public int selectId;
    public string response;

    public Response(int selectId, string response)
    {
        this.selectId = selectId;
        this.response = response;
    }
}