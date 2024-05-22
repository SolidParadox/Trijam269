using UnityEngine;

public class LightSensor : MonoBehaviour {
  public bool inLight = false;
  public bool transparent;      // PART OF ANOTHER SYSTEM : LightZone

  public float lightLevel;
  public float threshold;
  public float maxCapacity;

  private void Start () {
    lightLevel = 0;
  }

  private void Update () {
    if ( lightLevel > 0 ) {
      lightLevel -= Time.deltaTime;
      if ( lightLevel < 0 ) {
        lightLevel = 0;
      }
    }
    inLight = lightLevel >= threshold;
  }

  public void InLightPing ( float power ) {
    lightLevel += power;
    if ( lightLevel > maxCapacity ) {
      lightLevel = maxCapacity;
    }
  }

  public float GetRelativeBrightness () {
    return Mathf.Min ( 1, lightLevel / threshold );
  }
}
