using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFM : MonoBehaviour
{
    public GameObject bullet;

    public float fireDelay;

    private bool saCatch;
    private float deltaT;

    void LateUpdate()
    {
        if (Input.GetAxis("Fire1") > 0)
        {
            if (saCatch && deltaT <= 0)
            {
                Instantiate(bullet, transform.position + transform.forward * 0.1f, transform.rotation, SceneCore.Instance.spawnTransform);
                saCatch = false;
                deltaT = fireDelay;
            }
        } else
        {
            saCatch = true;
        }       
        if ( deltaT > 0 )
        {
            deltaT -= Time.deltaTime;
        }
    }
}
