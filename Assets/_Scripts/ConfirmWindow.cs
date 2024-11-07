using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmWindow : MonoBehaviour
{
    public Button conftitlebar;
    public Button confyes;
    public Button confno;

    public delegate void ConfirmYesEventHandler();
    public event ConfirmYesEventHandler OnConfirmYes;

    // Start is called before the first frame update
    void Start()
    {
        if (conftitlebar != null)
        {
            // Add a listener to the button's onClick event
            conftitlebar.onClick.AddListener(() => { this.gameObject.SetActive(false); });
        }

        if (confyes != null)
        {
            // Add a listener to the button's onClick event
            confyes.onClick.AddListener( FireConfirmYesEvent );
        }

        if (confno != null)
        {
            // Add a listener to the button's onClick event
            confno.onClick.AddListener(() => { this.gameObject.SetActive(false); });
        }
    }

    public void SetActive(bool setActive)
    {
        this.gameObject.SetActive(setActive);
    }

    // Method to fire the custom event
    void FireConfirmYesEvent()
    {
        if (OnConfirmYes != null)
        {
            OnConfirmYes.Invoke();
        }
        this.gameObject.SetActive(false);
    }
}
