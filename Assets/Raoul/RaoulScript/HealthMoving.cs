using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMoving : MonoBehaviour
{
    public RectTransform HealthBar;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(HealthBar != null)
        {
            RectTransform HealthBar = GetComponent<RectTransform>();
        }

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            HealthBar.anchoredPosition = new Vector2(HealthBar.anchoredPosition.x + 5f, HealthBar.anchoredPosition.y);
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            HealthBar.anchoredPosition = new Vector2(HealthBar.anchoredPosition.x - 5f, HealthBar.anchoredPosition.y);
        }

    }
}
