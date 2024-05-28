using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class SUPCableLayer : MonoBehaviour {
  public Transform[] anchors;
  public Transform placementAnchor;
  public GameObject stuff;

  private void Update () {
    for ( int i = 0; i < anchors.Length - 1; i++ ) {
      Debug.DrawLine ( anchors[i].position, anchors[i + 1].position, Color.yellow );
    }
  }

  public void Create () {
    for ( int i = 0; i < placementAnchor.childCount; i++ ) {
      DestroyImmediate ( placementAnchor.GetChild(i).gameObject ); 
    }
    HORCableCutter dhc = null;
    for ( int i = 0; i < anchors.Length - 1; i++ ) {
      GameObject delta = Instantiate ( stuff, placementAnchor );
      HORCableCutter dhc2 = delta.GetComponent<HORCableCutter> ();
      
      dhc2.edge.SetPoints ( new List<Vector2> { anchors[i].position, anchors[i + 1].position } );
      dhc2.SetFlowDirection ();

      if ( dhc != null ) {
        dhc.cable.linkA = dhc2.cable;
      }
      dhc = dhc2;
    }
  }
}
