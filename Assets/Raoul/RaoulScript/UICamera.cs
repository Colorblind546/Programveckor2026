using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICamera : MonoBehaviour
{
    public Camera mainCamera;
    public RenderTexture renderTexture;

    // Optional: if you want to capture automatically every frame (expensive)
    public bool captureEveryFrame = false;

    // Optional: you can read this from elsewhere after capture
    [HideInInspector] public Texture2D lastScreenshot;

    void Awake()
    {
        if (!mainCamera) mainCamera = GetComponent<Camera>();
        if (!mainCamera) mainCamera = Camera.main;
    }

    void OnEnable()
    {
        if (renderTexture != null && mainCamera != null)
        {
            // Assign once. Don't null/reassign every frame.
            mainCamera.targetTexture = renderTexture;
        }
    }

    void OnDisable()
    {
        if (mainCamera != null && mainCamera.targetTexture == renderTexture)
            mainCamera.targetTexture = null;
    }

    void Update()
    {
        if (renderTexture == null || mainCamera == null) return;

        // Press R to capture once (failsafe)
        if (Input.GetKeyDown(KeyCode.R))
            StartCoroutine(CaptureFromRenderTexture());

        // Takes a picture every frame
        if (captureEveryFrame)
            StartCoroutine(CaptureFromRenderTexture());
    }

    IEnumerator CaptureFromRenderTexture()
    {
        // Wait until the camera has rendered this frame
        yield return new WaitForEndOfFrame();

        // Make sure camera is rendering into your RT
        if (mainCamera.targetTexture != renderTexture)
            mainCamera.targetTexture = renderTexture;

        // Force a render if this camera isn't rendering every frame / is disabled
        // (Safe even if it already renders)
        mainCamera.Render();

        // Read from the RT that's actually assigned in the Inspector (600x256, etc.)
        var prev = RenderTexture.active;
        RenderTexture.active = renderTexture;

        // Reuse texture if possible
        if (lastScreenshot == null ||
            lastScreenshot.width != renderTexture.width ||
            lastScreenshot.height != renderTexture.height)
        {
            lastScreenshot = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        }

        lastScreenshot.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        lastScreenshot.Apply(false);

        RenderTexture.active = prev;

        
    }
}
