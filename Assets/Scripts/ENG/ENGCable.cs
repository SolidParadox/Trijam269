using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ENGCable : ENGPDS {
  public GameObject wirePrefab;
  public LineRenderer   edge;
  public EdgeCollider2D edgeCollider;

  private float matpos = 0;
  private Vector2 flowDirection;

  private void Start () {
    SetFlowDirection ();
  }

  public void SetFlowDirection () {
    flowDirection = Vector2.zero;
    if ( edge != null && edgeCollider != null ) {
      flowDirection = edge.GetPosition ( 1 ) - edge.GetPosition ( 0 );
      if ( flowDirection.magnitude < 0.5f ) {
        Destroy ( gameObject );
      }
      flowDirection.Normalize ();
      edgeCollider.SetPoints ( new List<Vector2> { edge.GetPosition ( 0 ), edge.GetPosition ( 1 ) } );
    }
  }

  private void Update () {
    if ( edge != null ) {
      edge.material.SetVector ( "_OFS", flowDirection * matpos * -0.0005f );
    }
    matpos += throughput * Time.deltaTime;
  }
  public void DealWithIntrerupt ( Vector2 point, float cl ) {
    Vector2 pMid = Vector3.Project ( point - (Vector2)edge.GetPosition ( 0 ), flowDirection );
    Vector2 pCP = pMid.normalized * 0.5f;
    Vector2 pOld1 = edge.GetPosition ( 1 );

    edge.SetPositions ( new Vector3[ 2 ] { edge.GetPosition ( 0 ), (Vector2) edge.GetPosition ( 0 ) + pMid - pCP } );
    SetFlowDirection ();
    IENGConnector cDelta = linkA;
    linkA = null;

    GameObject dgo =  Instantiate ( wirePrefab, transform.parent ); //(GameObject)PrefabUtility.InstantiatePrefab ( wirePrefab, transform.parent );
    ENGCable hcC = dgo.GetComponent<ENGCable>();

    hcC.edge.SetPositions ( new Vector3[2] { pOld1, (Vector2) edge.GetPosition ( 0 ) + pMid + pCP } );
    hcC.SetFlowDirection ();
    hcC.linkA = cDelta;
    hcC.Pass ( 0 );
  }
}
