using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{

    public UnityEvent functionEnter;
    public UnityEvent functionExit;
    public UnityEvent functionStay;
    public LayerMask triggerLayer;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if ((triggerLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            functionEnter.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((triggerLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            functionExit.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((triggerLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            functionStay.Invoke();
        }
    }
}
