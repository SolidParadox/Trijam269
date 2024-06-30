using UnityEngine;
using UnityEngine.Rendering;

public class UIEntryPulsar : MonoBehaviour {
  public Transform[]  targets;
  public float[]  frequency;
  public float[]  phaseDelta;
  public float[]  magnitude;
  private float[] spec;
  public float deltaT;

  public float randomCircleRadius;

  private void Start () {
    spec = new float[ frequency.Length ];
    //Debug.LogError ( SystemInfo.SupportsRenderTextureFormat ( RenderTextureFormat.ARGB32 ) );
  }

  void Update () {
    float deltaSpec;
    for ( int i = 0; i < targets.Length; i++ ) {
      if ( frequency[ i ] == 0 ) { continue; }
      deltaSpec = spec[ i ];
      spec[i] = Mathf.Repeat ( phaseDelta[i] + deltaT, frequency[i] ) / frequency[i];
      if ( spec[i] < deltaSpec ) {
        targets[i].position = Random.insideUnitCircle * randomCircleRadius;
      }
      targets[i].localScale = Vector3.forward + magnitude [ i ] * new Vector3 ( 1, 1, 0 ) * spec [ i ];
    } 
    deltaT += Time.deltaTime;
  }
}
