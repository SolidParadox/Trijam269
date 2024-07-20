using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class SUPCableLayer : MonoBehaviour {
  public Transform[] anchors;
  public Transform placementAnchor;
  public GameObject stuff;

#if UNITY_EDITOR

  private void Update () {
    for ( int i = 0; i < anchors.Length - 1; i++ ) {
      Debug.DrawLine ( anchors[i].position, anchors[i + 1].position, Color.yellow );
    }
  }

  public void Create () {
    for ( int i = 0; i < placementAnchor.childCount; i++ ) {
      DestroyImmediate ( placementAnchor.GetChild ( i ).gameObject );
    }
    ENGCable cableDelta, cableDelta2 = null;
    for ( int i = 0; i < anchors.Length - 1; i++ ) {
      GameObject delta = (GameObject)PrefabUtility.InstantiatePrefab ( stuff, placementAnchor );
      cableDelta = delta.GetComponent<ENGCable> ();

      cableDelta.edge.SetPositions ( new Vector3[] { anchors[i].position, anchors[i + 1].position } );
      cableDelta.SetFlowDirection ();

      if ( cableDelta2 != null ) {
        cableDelta2.linkA = cableDelta;
      }
      cableDelta2 = cableDelta;
    }
  }
#endif
}
