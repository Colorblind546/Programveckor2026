using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public bool loaded;
    public bool damage;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttackInputRegisterer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireWeapon();
        }
    }

    public virtual void FireWeapon()
    {

    }

    public void Reload()
    {
        loaded = true;
    }
}
