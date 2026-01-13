using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICamera : MonoBehaviour
{
    public Camera mainCamera;
    public RenderTexture renderTexture;

    // Start is called before the first frame update
    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Or GetComponent<Camera>();
        }

        // Set up a Render Texture (e.g., for Post-Processing or UI)
        if (renderTexture != null)
        {
            mainCamera.targetTexture = renderTexture;
        }
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.R))
        {
            // Reset the Render Texture on key press
            if (renderTexture != null)
            {
                mainCamera.targetTexture = null; // Clear the target texture
                mainCamera.targetTexture = renderTexture; // Reassign it
            }
            TakeScreenshot();
        }
    }


    void TakeScreenshot()
    {
         RenderTexture tempRT = new RenderTexture(renderTexture.width, renderTexture.height, 24); 
        mainCamera.targetTexture = tempRT; 
        Texture2D screenshot = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = tempRT;
        screenshot.ReadPixels(new Rect(0, 0, tempRT.width, tempRT.height), 0, 0);
        screenshot.Apply();
        RenderTexture.active = null;
        mainCamera.targetTexture = null;
        Destroy(tempRT);
    }
}
