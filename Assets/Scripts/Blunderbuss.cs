using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blunderbuss : Weapon
{

    public float spread;
    public int pelletCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        AttackInputRegisterer();




    }

    public override void FireWeapon()
    {

        for (int i = 0; i < pelletCount; i++)
        {


            Vector3 spreadDirection = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);

            Quaternion bulletOffset = Quaternion.Euler(spreadDirection * spread);

            Vector3 fireDirection = (Camera.main.transform.localRotation * bulletOffset) * transform.forward;

            if (Physics.Raycast(Camera.main.transform.position, fireDirection, out RaycastHit hit, 0, enemyLayer))
            {
                
            }


        }


    }


}
