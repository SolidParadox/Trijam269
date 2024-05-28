using System.Collections.Generic;
using UnityEngine;

public class HORCableCutter : MonoBehaviour {
  public ENGCable cable;

  public GameObject wirePrefab;
  public RadarCore intreruptRadar;

  public EdgeCollider2D edge;
  public LineRenderer   edgeRender;

  private Vector2 flowDirection;

  public float length;
  public float  minLength = 5;
  private float matpos = 0;

  void Start () {
    SetFlowDirection();
  }
  
  public void SetFlowDirection () {
    flowDirection = edge.points[1] - edge.points[0];
    length = flowDirection.magnitude;
    edgeRender.SetPositions ( new Vector3[ 2 ] { edge.points[ 0 ], edge.points[ 1 ] } );

    if ( length < minLength ) {
      Destroy ( gameObject );
    }
    flowDirection.Normalize ();
  }

  void Update () {
    matpos += cable.throughput * Time.deltaTime;
    edgeRender.material.SetVector ( "_OFS", flowDirection * matpos * -0.001f );
    DealWithIntrerupt ();
  }

  public void DealWithIntrerupt () {
    if ( intreruptRadar.breached ) {
      RaycastHit2D[] rh = Physics2D.RaycastAll ( edge.points[0], flowDirection, length );
      for ( int i = 0; i < rh.Length; i++ ) {
        if ( intreruptRadar.contacts.Contains ( rh[ i ].collider.gameObject ) ) {
          Vector2 pDelta = edge.points [ 1 ];
          Vector2 pMid = Vector3.Project ( rh [ i ].point - edge.points [ 0 ], flowDirection );
          Vector2 pCP = pMid.normalized * 0.5f;

          Debug.DrawLine ( edge.points[0] + pMid, edge.points[0] + pMid - pCP, Color.green, 1000 );

          edge.SetPoints( new List<Vector2> { edge.points[ 0 ], edge.points[0] + pMid - pCP } );
          SetFlowDirection ();
          IENGConnector cDelta = cable.linkA;
          cable.linkA = null;

          HORCableCutter hcC = Instantiate ( wirePrefab ).GetComponent<HORCableCutter>();

          hcC.edge.SetPoints ( new List<Vector2> { edge.points[0] + pMid + pCP, pDelta } );
          hcC.SetFlowDirection ();
          hcC.cable.linkA = cDelta;
          hcC.cable.Pass ( 0 );

          break;
        }
      }
    }
  }
}
