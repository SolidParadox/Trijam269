using UnityEngine;

public class LightSource : MonoBehaviour {
  public RadarCore directLightRadar;
  public RRays rays;
  public float power = 1;

  private LightSensor ls;

  public MeshFilter mf;
  private Mesh mesh;

  private Vector3[] vertices; // Vertices of the mesh
  private int[] triangles; // Triangles of the mesh
  private int vertexIndex = 0; // Current vertex index

  private void Start () {
    mesh = new Mesh ();
    mf.mesh = mesh;
    mf.transform.position = Vector3.zero;
  }

  void LateUpdate () {
    for ( int i = 0; i < directLightRadar.contacts.Count; i++ ) {
      float relativePower = 1 - ( transform.position - directLightRadar.contacts[i].transform.position ).sqrMagnitude / ( rays.distance * rays.distance );
      if ( directLightRadar.contacts[i].TryGetComponent ( out ls ) ) {
        ls.InLightPing ( relativePower * power * Time.deltaTime );
      }
    }

    rays.WorkFunction ();

    vertices = new Vector3[rays.rayCount + 1];
    Vector2[] uv = new Vector2[rays.rayCount + 1];
    
    triangles = new int[( rays.rayCount - 1 ) * 3];
    
    vertices[0] = Vector2.zero;
    uv[0] = Vector2.zero;

    vertexIndex = 1;

    for ( int i = 0; i < rays.rayCount; i++ ) {
      vertices[vertexIndex] = rays.points [ i ];
      uv[vertexIndex] = Vector2.one * rays.GetDistance( i );
      if ( vertexIndex >= 2 ) {
        triangles[( vertexIndex - 2 ) * 3] = 0;
        triangles[( vertexIndex - 2 ) * 3 + 1] = vertexIndex;
        triangles[( vertexIndex - 2 ) * 3 + 2] = vertexIndex - 1;
      }
      vertexIndex++;
    }
    mesh.vertices = vertices;
    mesh.triangles = triangles;
    mesh.uv = uv;
    mesh.RecalculateNormals ();
    mesh.RecalculateBounds ();
  }
}
