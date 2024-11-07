using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamApp : MonoBehaviour
{
    public Camera sourceCamera; // The camera to render
    public RawImage targetRawImage; // The UI RawImage to display the camera view
    public RenderTexture renderTexture; // The RenderTexture to use

    // Start is called before the first frame update
    void Start()
    {
        if (sourceCamera != null && targetRawImage != null && renderTexture != null)
        {
            // Assign the RenderTexture to the camera
            sourceCamera.targetTexture = renderTexture;
            Debug.Log("Assigned RenderTexture to sourceCamera");

            // Assign the RenderTexture to the RawImage
            targetRawImage.texture = renderTexture;
            Debug.Log("Assigned RenderTexture to targetRawImage");

            // Ensure the sourceCamera is enabled
            sourceCamera.enabled = true;
            Debug.Log("Enabled sourceCamera");

        }
        else
        {
            Debug.LogError("Please assign all the required fields in the Inspector.");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
