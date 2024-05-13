using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMAN : MonoBehaviour
{
    public RadarCore radar;
    void Update()
    {
        if ( radar.breached )
        {
            Destroy(gameObject);
        }        
    }
}
