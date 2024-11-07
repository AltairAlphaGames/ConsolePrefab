using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

using TMPro;

public class EmailApp : MonoBehaviour
{
    public TMP_Text titletext;  // The ListItem prefab
    public Button close;  // The ListItem prefab
    public TMP_Dropdown mailboxselect;  // The ListItem prefab
    public GameObject listItemPrefab;  // The ListItem prefab
    public Transform listContentPanel;     // The Content panel in the Scroll View
    public GameObject header;     // The Content panel in the Scroll View
    public GameObject body;     // The Content panel in the Scroll View

    private UserData userData;
    private TextAsset emailFile;

    // Start is called before the first frame update
    void Start()
    {
        if (close != null)
        {
            close.onClick.AddListener(OnCloseClick);
        }    

        if (mailboxselect != null)
        {
            if (mailboxselect != null)
            {
                mailboxselect.onValueChanged.AddListener(OnMailboxSelect);
            }
        }
        
    }

    void OnCloseClick()
    {
        this.gameObject.SetActive(false);
    }

    void OnMailboxSelect(int selectedIndex)
    {

        string selectedOption = mailboxselect.options[selectedIndex].text;

        populateEmailList(selectedIndex);
    }

    public void SetMailbox(int mb)
    {
        this.OnMailboxSelect(mb);
    }

    public void SetActive(bool setActive)
    {
        this.gameObject.SetActive(setActive);
    }

    public void ProcessEmailFile( TextAsset f )
    {
        this.emailFile = f;

        this.userData = this.ParseEmailJson();
        titletext.text = "Email - " + userData.username;
    }

    UserData ParseEmailJson()
    {
        return JsonUtility.FromJson<UserData>(emailFile.text); 
    }

    void populateEmailList(int mailboxIdx = 0)
    {
        List<EmailData> mailbox;

        ClearScrollView(listContentPanel);

        switch (mailboxIdx)
        {
            case 1 :
                mailbox = userData.emails.sentitems;
            break;
            case 2 :
                mailbox = userData.emails.trash;
            break;
            default:
                mailbox = userData.emails.inbox;
            break;
        }

        foreach (EmailData email in mailbox)
        {
            GameObject listItem = Instantiate(listItemPrefab, listContentPanel); 
            Button btn = listItem.GetComponent<Button>();
            btn.onClick.AddListener(() => OnEmailSelected(email));
            TMP_Text[] textFields = listItem.GetComponentsInChildren<TMP_Text>(); 
            
            DateTime utcDateTime = DateTime.Parse(email.date, null, System.Globalization.DateTimeStyles.RoundtripKind);

            textFields[0].text = "From: " + email.from + "\nSubject: " + email.subject + "\nDate: " + utcDateTime.ToString("yyyy-MM-dd hh:mm:ss");
            //listItem.GetComponent<ListItem>().SetContent(email.from, email.subject, email.date.ToString("dd/MM/yyyy"));
        }
    }

    void OnEmailSelected(EmailData email)
    {
        DateTime utcDateTime = DateTime.Parse(email.date, null, System.Globalization.DateTimeStyles.RoundtripKind);

        TMP_Text[] headerFields = header.GetComponentsInChildren<TMP_Text>();
        TMP_Text[] bodyFields = body.GetComponentsInChildren<TMP_Text>();

        headerFields[0].text = "From: " + email.from;
        headerFields[0].text += "\nTo: " + email.to;
        headerFields[0].text += "\nSubject: " + email.subject;
        
        headerFields[1].text = "Recieved: " + utcDateTime.ToString("yyyy-MM-dd hh:mm:ss");

        bodyFields[0].text = email.body;
    }

    public void ClearScrollView(Transform contentPanel)
    {
        // Loop through all the children of the content panel and destroy them
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }
    }

}

//Parse email
[System.Serializable]
public class EmailData
{
    public string from;
    public string to;
    public string subject;
    public string body;
    public string date;

    public EmailData(string from, string to, string subject, string body, string date)
    {
        this.from = from;
        this.to = to;
        this.from = from;
        this.subject = subject;
        this.body = body;
        this.date = date;
    }
}

[System.Serializable]
public class UserData
{
    public string username;
    public EmailFolder emails;

    public UserData(string username, EmailFolder emails)
    {
        this.username = username;
        this.emails = emails;
    }
}

[System.Serializable]
public class EmailFolder
{
    public List<EmailData> inbox;
    public List<EmailData> sentitems;
    public List<EmailData> trash;

    public EmailFolder(List<EmailData> inbox, List<EmailData> sentitems, List<EmailData> trash)
    {
        this.inbox = inbox;
        this.sentitems = sentitems;
        this.trash = trash;
    }
}

