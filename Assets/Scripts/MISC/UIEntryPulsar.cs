using UnityEngine;
using UnityEngine.Rendering;

public class UIEntryPulsar : MonoBehaviour {
  public Transform[]  targets;
  public float[]  frequency;
  public float[]  phaseDelta;
  public float[]  magnitude;
  public float deltaT;

  void Update () {
    for ( int i = 0; i < targets.Length; i++ ) {
      if ( frequency[ i ] == 0 ) { continue; }
      targets[i].localScale = magnitude [ i ] * Vector3.one * Mathf.Repeat ( phaseDelta[i] + deltaT, frequency[i] ) / frequency [ i ];
    }
    deltaT += Time.deltaTime;
  }
}
