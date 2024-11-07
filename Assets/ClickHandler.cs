using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickHandler : MonoBehaviour
{
    public Button emailButton;
    public Button controlPanelButton;
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
        if (controlPanelButton != null)
        {
            // Add a listener to the button's onClick event
            controlPanelButton.onClick.AddListener(OnControlPanelClick);
        }
    }

    // Method to handle button click
    void OnEmailClick()
    {
        Debug.Log("Email was clicked!");
    }

    // Method to handle button click
    void OnControlPanelClick()
    {
        Debug.Log("Control panel was clicked!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
