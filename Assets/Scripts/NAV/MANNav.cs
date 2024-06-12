using System.Collections.Generic;
using UnityEngine;

public class MANNav : MonoBehaviour {
  public PolygonCollider2D[] rooms;
  [System.Serializable]
  public struct Gate {
    public Transform coord;
    public int a, b;
    public bool closed;
  }

  public Gate[] gates;

  [System.Serializable]
  public class Node {
    public int value;
    public Vector2 pos;
    public List<Node> neighbours;
    public Node() {  value = 0; neighbours = new List<Node> (); pos = Vector2.zero; }
  }
  public List<Node> nodes;

  private void Start () {
    GenerateNodes ();
  }

  private void OnGUI () {
    for ( int i = 0; i < gates.Length; i++ ) {
      Debug.DrawLine ( nodes[gates[i].a].pos, gates[ i ].coord.position, Color.red, 300 );
      Debug.DrawLine ( gates[i].coord.position, nodes[gates[i].b].pos, Color.red, 300 );
    }
  }

  public int GetRoom ( Vector2 alpha ) {
    for ( int i = 0; i < rooms.Length; i++ ) {
      if ( rooms[i].OverlapPoint ( alpha ) ) {
        return i;
      }
    }
    return -1;
  }

  public Vector2 GetPath ( Vector2 a, Vector2 b ) {
    int ra = GetRoom ( a ), rb = GetRoom ( b );
    if ( ra == rb ) { return b - a; }

    int cidx = 0;
    List<int> deltaC = new List<int>();
    deltaC.Add ( ra );

    while ( deltaC.Count > cidx ) {
      if ( deltaC[ cidx ] == rb ) {

        break;
      } else {

      }
      cidx++;
    }

    return Vector2.zero;
  }

  public void GenerateNodes () {
    nodes = new List<Node>();
    Node deltaNode;
    for ( int i = 0; i < rooms.Length; i++ ) {
      deltaNode = new Node ();
      Vector2 dc = Vector2.zero;
      for ( int j = 0; j < rooms[ i ].points.Length; j++ ) {
        dc += rooms[i].points[j];
      }
      deltaNode.pos = dc / rooms[i].points.Length;
      nodes.Add ( deltaNode );
    }
    for ( int i = 0; i < gates.Length; i++ ) {
      deltaNode = new Node ();
      deltaNode.pos = gates [ i ].coord.position;
      deltaNode.neighbours.Add ( nodes[gates[i].a] );
      deltaNode.neighbours.Add ( nodes[gates[i].b] );
      nodes.Add ( deltaNode );
      nodes[gates[i].a].neighbours.Add ( nodes[ nodes.Count - 1 ] );
      nodes[gates[i].b].neighbours.Add ( nodes[nodes.Count - 1] );
    }
  }
}
