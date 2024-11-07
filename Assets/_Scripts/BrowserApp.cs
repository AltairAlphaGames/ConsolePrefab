using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrowserApp : MonoBehaviour
{
    public Button titlebar;

    // Start is called before the first frame update
    void Start()
    {
        if (titlebar != null)
        {
            // Add a listener to the button's onClick event
            titlebar.onClick.AddListener(() => { this.gameObject.SetActive(false); });
        }
    }

    public void SetActive(bool setActive)
    {
        this.gameObject.SetActive(setActive);
    }


}
