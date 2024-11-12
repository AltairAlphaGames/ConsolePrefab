using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

using TMPro;

public class Term1 : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] public Transform player;
    [SerializeField] public string ownerCharacter = "Sarah Peterson";
    [SerializeField] public TextAsset emailFile;
    [SerializeField] public TextAsset argusFile;
    [SerializeField] public float detectionRange = 2f;  // Distance within which the menu can be opened


    [Header("Shutdown Confirmation")]
    public ConfirmWindow shutdownconf;
    public Button powerbtn;

    [Header("Icon Handlers")]
    public Button browserButton;
    public Button emailButton;
    public Button argusButton;

    [Header("App References")]
    public EmailApp emailApp;
    public BrowserApp browserApp;
    public ArgusApp argusApp;

    private Conversation conversation;
    private bool isMenuOpen = false;  // Flag to check if the menu is open

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the button is assigned
        if (emailButton != null)
        {
            // Add a listener to the button's onClick event
            emailButton.onClick.AddListener(OnEmailClick);
        }

        // Ensure the button is assigned
        if (browserButton != null)
        {
            // Add a listener to the button's onClick event
            browserButton.onClick.AddListener(OnBrowserClick);
        }    

        // Ensure the button is assigned
        if (argusButton != null)
        {
            // Add a listener to the button's onClick event
            argusButton.onClick.AddListener(OnArgusClick);
        }

        // Ensure the button is assigned
        if (powerbtn != null)
        {
            // Add a listener to the button's onClick event
            powerbtn.onClick.AddListener(() => 
            { 
                shutdownconf.SetActive(true); 
                shutdownconf.OnConfirmYes += CloseTerminal;
            });
        }

        OpenMenu();
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle menu when pressing the Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseTerminal();
        }
    }

    void CloseTerminal()
    {
        if (isMenuOpen)
        {
            ResumeGame();
        }
    }

    public void OpenMenu()
    {
        this.gameObject.SetActive(true);  // Show the menu
        isMenuOpen = true;
        //Time.timeScale = 0f;  // Pause the game
        Cursor.lockState = CursorLockMode.None;  // Unlock the cursor
        Cursor.visible = true;  // Show the cursor
    }
    

    public void ResumeGame()
    {
        this.gameObject.SetActive(false);  // Hide the menu
        isMenuOpen = false;
        //Time.timeScale = 1f;  // Resume the game
        Cursor.lockState = CursorLockMode.Locked;  // Lock the cursor again
        Cursor.visible = false;  // Hide the cursor
    }

    public bool IsMenuOpen()
    {
        return isMenuOpen;
    }

    // Method to handle button click
    void OnEmailClick()
    {
        emailApp.ProcessEmailFile( emailFile );
        emailApp.SetMailbox(0);

        emailApp.SetActive(true);
    }

    // Method to handle button click
    void OnBrowserClick()
    {
        emailApp.SetActive(false);
        browserApp.SetActive(true);
    }

    // Method to handle button click
    void OnArgusClick()
    {
        argusApp.SetTerminalOwner( this.ownerCharacter );
        argusApp.SetEcounterId(0);
        argusApp.SetConversationId(0);

        argusApp.SetActive(true);
    }

    // This method is called when the user clicks on the object
    void OnMouseDown()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        if (distanceToPlayer <= detectionRange && !isMenuOpen)
        {
            OpenMenu();
        }
    }
}

