using UnityEngine;
using System.Collections.Generic;

public class LightZone : MonoBehaviour {
  public RRays rays;

  public float penetrationRange = 0.1f;

  public MeshFilter mf;
  public MeshRenderer mr;
  private Mesh mesh;

  private Vector3[] vertices;
  private int[] triangles;
  private int vertexIndex = 0;

  public Color spectrum = Color.white;

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

    mr.material.SetColor ( "_Spectrum", spectrum );

    vertices[0] = Vector3.zero;
    Vector2[] uv = new Vector2[rays.rayCount + 1];
    uv[0] = new Vector2 ( 0.5f, 0.5f );
    vertexIndex = 1;

    List<Collider2D> touched = new List<Collider2D> ();

    for ( int i = 0; i < rays.rayCount; i++ ) {
      //Debug.DrawLine ( Vector3.zero, rays.points[i] );
      uv[vertexIndex] = new Vector2 ( 0.5f, 0.5f ) + 0.5f * rays.points[i] / ( rays.distance + penetrationRange );
      Vector2 transformedPoint = transform.InverseTransformDirection ( rays.points[i] + rays.points[i].normalized * penetrationRange );
      vertices[vertexIndex] = transformedPoint;
       
      if ( vertexIndex >= 2 ) {
        triangles[( vertexIndex - 2 ) * 3] = 0;
        triangles[( vertexIndex - 2 ) * 3 + 1] = vertexIndex;
        triangles[( vertexIndex - 2 ) * 3 + 2] = vertexIndex - 1;
      }
      vertexIndex++;
    }

    mesh.Clear ();
    // Update the mesh with new vertices and triangles
    mesh.vertices = vertices;
    mesh.triangles = triangles;
    mesh.SetUVs ( 0, new List<Vector2> ( uv ) );

    // Recalculate normals and bounds
    mesh.RecalculateNormals ();
    mesh.RecalculateBounds ();
  }

  private void OnDisable () {
    mr.material.SetColor ( "_Spectrum", Color.black );
  }
}