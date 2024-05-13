using UnityEngine;

public class LightSensor : MonoBehaviour
{
    public bool inLight = false;
    public float delayTimer;
    private float deltaT = 0;

    public bool transparent;

    private void Update()
    {
        if ( deltaT > 0 )
        {
            deltaT -= Time.deltaTime;
            if ( deltaT <= 0 )
            {
                inLight = false;
            }
        }
    }

    public void InLightPing ()
    {
        inLight = true;
        deltaT = delayTimer;
    }
    public float GetRelativeBrightness ()
    {
        if (delayTimer == 0) return 0;
        if ( !inLight) return 0;
        return deltaT / delayTimer;
    }
}
