using UnityEngine;

public class LightZone : MonoBehaviour
{
    public float angle;
    public int rays;

    public float distance;

    public ContactFilter2D contactFilter;

    public MeshFilter mf;
    private Mesh mesh;

    private Vector3[] vertices; // Vertices of the mesh
    private int[] triangles; // Triangles of the mesh
    private int vertexIndex = 0; // Current vertex index

    private LightSensor deltaLS;

    private void Start()
    {
        mesh = new Mesh();
        mf.mesh = mesh;
        mf.transform.position = Vector3.zero;
    }

    void FixedUpdate()
    {
        if (rays == 0) { return; }

        Vector2 heading = Quaternion.Euler(0, 0, -angle / 2) * transform.up;
        Quaternion headingDelta = Quaternion.Euler(0, 0, angle / rays);
        int deltaHC = 0;
        RaycastHit2D[] results = new RaycastHit2D[16];

        vertices = new Vector3[rays + 1];
        triangles = new int[(rays - 1) * 3];
        vertices[0] = transform.position;
        vertexIndex = 1;

        float fpd;

        for (int i = 0; i < rays; i++)
        {
            deltaHC = Physics2D.Raycast(transform.position, heading, contactFilter, results);
            Debug.DrawLine(transform.position, transform.position + (Vector3)heading * 10, Color.cyan);

            fpd = distance;

            if (deltaHC != 0)
            {

                for (int j = 0; j < deltaHC; j++)
                {
                    if (results[j].collider.TryGetComponent(out deltaLS) && deltaLS.transparent )
                    {
                        continue;
                    }
                    if (results[j].distance < fpd)
                    {
                        fpd = results[j].distance;
                    }
                }
                for (int j = 0; j < deltaHC; j++)
                {
                    if (results[j].distance <= fpd)
                    {
                        if (results[j].collider.GetComponent<LightSensor>() != null)
                        {
                            results[j].collider.GetComponent<LightSensor>().InLightPing();
                        }
                        Debug.DrawLine(transform.position, results[j].point, Color.red);
                        Debug.DrawLine(results[j].point, results[j].point + new Vector2(0, 0.2f), Color.green);
                    }
                }
            }


            vertices[vertexIndex] = heading * fpd + (Vector2)transform.position;
            if (vertexIndex >= 2)
            {
                triangles[(vertexIndex - 2) * 3] = 0;
                triangles[(vertexIndex - 2) * 3 + 1] = vertexIndex - 1;
                triangles[(vertexIndex - 2) * 3 + 2] = vertexIndex;
            }
            vertexIndex++;

            heading = headingDelta * heading;
        }

        // Update the mesh with new vertices and triangles
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        // Recalculate normals and bounds
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}
