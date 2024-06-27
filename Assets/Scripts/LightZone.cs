using UnityEngine;
using System.Collections.Generic;

public class LightZone : MonoBehaviour {
  public RRays rays;

  public MeshFilter mf;
  public MeshRenderer mr;
  private Mesh mesh;

  private Vector3[] vertices;
  private int[] triangles;
  private int vertexIndex = 0;

  private void Start () {
    mesh = new Mesh ();
    mf.mesh = mesh;
    mr.material = new Material ( mr.material );
    rays.overrideFUW = true;
    vertices = new Vector3[ rays.rayCount + 1 ];
    triangles = new int[rays.rayCount * 3];
  }

  void FixedUpdate () {
    rays.WorkFunction ();

    mr.material.SetFloat ( "_SM", 1 );

    vertices[0] = Vector2.zero;
    Vector2[] uv = new Vector2[rays.rayCount + 1];
    uv[0] = Vector2.zero;
    vertexIndex = 1;

    List<Collider2D> touched = new List<Collider2D> ();

    for ( int i = 0; i < rays.rayCount; i++ ) {
      Debug.DrawLine ( transform.position, rays.points[i] );
      vertices[vertexIndex] = transform.InverseTransformDirection ( rays.points[i] - (Vector2)( transform.position ) );
      //uv[vertexIndex] = new Vector2 ( ( vertexIndex - 1 ) / (float)rays, fpd / distance );
      uv[vertexIndex] = Vector2.one * rays.GetDistance( i );
      if ( vertexIndex >= 2 ) {
        triangles[( vertexIndex - 2 ) * 3] = 0;
        triangles[( vertexIndex - 2 ) * 3 + 1] = vertexIndex;
        triangles[( vertexIndex - 2 ) * 3 + 2] = vertexIndex - 1;
      }
      vertexIndex++;
    }

    // Update the mesh with new vertices and triangles
    mesh.vertices = vertices;
    mesh.triangles = triangles;
    mesh.uv = uv;

    // Recalculate normals and bounds
    mesh.RecalculateNormals ();
    mesh.RecalculateBounds ();
  }
}