using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler 
{
    public Vector3 pressedScale = new Vector3(0.95f, 0.95f, 0.95f);
    public float speed = 12f;

    Vector3 startScale;
    bool pressed;

    public GameObject gameObjectt;
    public GameObject gameObjectt2;
    public GameObject gameObjectt3;
    public GameObject gameObjectt4;
    public GameObject gameObjectt5;
    public GameObject gameObjectt6;

    void Awake()
    {
        startScale = transform.localScale;
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

    public void Change()
    {
        gameObject.SetActive(true);
        gameObjectt2.SetActive(true);
        gameObjectt3.SetActive(true);
        gameObjectt4.SetActive(false);
        gameObjectt5.SetActive(false);
        gameObjectt6.SetActive(false);
    }
}
