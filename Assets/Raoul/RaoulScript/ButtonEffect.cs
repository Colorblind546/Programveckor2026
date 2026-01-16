using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler 
{
    public Vector3 pressedScale = new Vector3(0.95f, 0.95f, 0.95f);
    public float speed = 12f;

    Vector3 startScale;
    bool pressed;

 

    // New fields for hover sprite swapping
    public Image targetImage;
    public Sprite normalSprite;
    public Sprite hoverSprite;

    void Awake()
    {
        startScale = transform.localScale;

        // Try to find an Image component on this GameObject if none provided
        if (targetImage == null)
        {
            targetImage = GetComponent<Image>();
        }

        // If no normalSprite provided, cache current sprite from the Image (if available)
        if (targetImage != null && normalSprite == null)
        {
            normalSprite = targetImage.sprite;
        }
    }

    void Update()
    {
        Vector3 target = pressed ? pressedScale : startScale;
        transform.localScale = Vector3.Lerp(
            transform.localScale, target, Time.deltaTime * speed
        );
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
    }

    // Swap to hover sprite when pointer enters
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (targetImage != null && hoverSprite != null)
        {
            targetImage.sprite = hoverSprite;
        }
    }

    // Restore normal sprite when pointer exits
    public void OnPointerExit(PointerEventData eventData)
    {
        if (targetImage != null && normalSprite != null)
        {
            targetImage.sprite = normalSprite;
        }
    }

  
}
