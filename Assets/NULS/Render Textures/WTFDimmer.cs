using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WTFDimmer : MonoBehaviour {
  public SpriteRenderer sr;
  public float timer;
  private float deltaT = 0;
  // Update is called once per frame
  void Update () {
    deltaT += Time.deltaTime;
    sr.color = Color.Lerp ( Color.white, Color.black, Mathf.Repeat( deltaT, timer ) / timer );
  }
}
