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

  public class Node {
    public float value;
    public Vector2 pos;
    public List<Node> neighbours;
    public Node() {  value = 0; neighbours = new List<Node> (); pos = Vector2.zero; }
  }
  public List<Node> nodes;

  private void Start () {
    GenerateNodes ();
  }

  private void OnGUI () {
    return;
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
  public Vector2 GetPath ( Vector2 start, Vector2 end ) {
    int startRoom = GetRoom(start);
    int endRoom = GetRoom(end);

    if ( startRoom == endRoom && startRoom != -1 ) {
      return end - start;
    }

    Dictionary<Node, Node> predecessors = new Dictionary<Node, Node>();

    foreach ( var node in nodes ) {
      node.value = float.PositiveInfinity; // Set to a high value to indicate unvisited
    }

    Node startNode = nodes [ startRoom ];
    Node endNode = nodes [ endRoom ];

    if ( startNode == null || endNode == null ) {
      return Vector2.zero; 
    }

    startNode.value = 0;
    Queue<Node> openList = new Queue<Node>();
    openList.Enqueue ( startNode );

    while ( openList.Count > 0 ) {
      Node currentNode = openList.Dequeue();
      if ( currentNode == endNode ) {
        break;
      }
      foreach ( Node neighbor in currentNode.neighbours ) {
        if ( neighbor.value == float.PositiveInfinity ) { // Check if the node is unvisited
          neighbor.value = currentNode.value + ( neighbor.pos - currentNode.pos ).magnitude;
          predecessors[neighbor] = currentNode; // Track the path
          openList.Enqueue ( neighbor );
        }
      }
    }

    List<Vector2> path = new List<Vector2>();
    Node pathNode = endNode;

    if ( !predecessors.ContainsKey ( endNode ) ) {
      return Vector2.zero;
    }

    while ( predecessors[pathNode] != startNode ) {
      pathNode = predecessors[pathNode];
    }

    return pathNode.pos;
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
