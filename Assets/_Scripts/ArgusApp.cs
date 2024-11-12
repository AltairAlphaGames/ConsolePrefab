using System;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class ArgusApp : MonoBehaviour
{
    public Button close;  // The ListItem prefab
    public float typingSpeed = 0.05f;
    public GameObject argusResponsePrefab;  // The ListItem prefab
    public GameObject ListSepPrefab;  // The ListItem separator prefab
    public ScrollRect argusScrollRect;     // The Content panel in the Scroll View
    public Transform argusContentPanel;     // The Content panel in the Scroll View
    public TMP_Text argusText;     // The Content panel in the Scroll View

    private string ownerCharacter = "Unknown";
    private TextAsset argusFile;
    private int encounterId;     // Determines the encounter ID to use
    private int converstationId;     // Determines the conversation ID to use

    private T1Dialogue dialogue;

    // Start is called before the first frame update
    void Start()
    {
        if (this.close != null)
        {
            this.close.onClick.AddListener(OnCloseClick);
        }

        this.dialogue = new T1Dialogue(0, 0);

        this.ClearText();
        // Display the first node
        this.executeArgusGreeting();
        //DisplayNode(this.dialogue.GetFirstNode());
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

    public void SetEcounterId(int id)
    {
        this.encounterId = id;
    }

    public void SetConversationId(int id)
    {
        this.converstationId = id;
    }

    public void SetTerminalOwner( string o )
    {
        this.ownerCharacter = o;
    }

    public void ProcessArgusFile( TextAsset f )
    {
        this.argusFile = f;

        //DialogueSetup().Start();
    }

    public void ClearScrollView(Transform contentPanel)
    {
        // Loop through all the children of the content panel and destroy them
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }
    } 

    public void executeArgusGreeting()
    {
        String greeting = this.dialogue.GetFirstNode().Text.Replace("[TerminalOwner]", this.ownerCharacter );

        StartCoroutine(TypeText(greeting, this.dialogue.GetFirstNode()));

        //DisplayNode(this.dialogue.GetFirstNode());
    }

    System.Collections.IEnumerator TypeText(string typeText, DialogueNode node)
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

        DisplayNode(node);
        //populateResponseList();
    }

    void DisplayNode(DialogueNode node)
    {
        // Display node text
        Debug.Log(node.Options.Count);

        if (node.Options.Count == 0)
        {
            DisplayNode(this.dialogue.GetFirstNode());
            return; 
        }

        // Display options
        foreach (DialogueOption option in node.Options)
        {
            //Debug.Log("Option: " + option.Text);

            GameObject listItem = Instantiate(argusResponsePrefab, argusContentPanel); 
            Button btn = listItem.GetComponent<Button>();
            btn.onClick.AddListener(() => ExecuteOptionSelection(option, option.NextNode));
            TMP_Text[] textFields = listItem.GetComponentsInChildren<TMP_Text>(); 
            
            textFields[0].text = option.Text.Replace("[TerminalOwner]", ownerCharacter);
        }
    }

    void ExecuteOptionSelection(DialogueOption opt, DialogueNode dlgNode)
    {
        ClearScrollView(argusContentPanel);

        StartCoroutine(TypeText(dlgNode.Text.Replace("[TerminalOwner]", ownerCharacter), dlgNode));
    }

}
