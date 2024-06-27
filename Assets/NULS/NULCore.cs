using UnityEngine;

public class NULCore : MonoBehaviour {
  public ComputeShader zdShader;
  public RenderTexture lfTexture;   // Light Field
  public RenderTexture printTexture;

  public Transform DEBUGdemonstrator;
  public Transform DEBUGdemonstrator2;
  public float speed;
  private float deltaPos;

  public Vector3 deltas;
  public float strength;
  public float radius;

  void Start () {
    zdShader.SetTexture ( 0, "LFSample", lfTexture );
    zdShader.SetTexture ( 0, "Result", printTexture );
    deltaPos = 0;
  }

  void Update () {
    //zdShader.SetVector ( "dimColor", dimColor );
    zdShader.SetVector ( "dimColour", new Vector4 ( deltas.x, deltas.y, deltas.z, 0 ) * strength * Time.deltaTime );
    zdShader.Dispatch ( 0, lfTexture.width / 8, lfTexture.height / 8, 1 );

    //DEBUGdemonstrator.position = new Vector2 ( Mathf.Sin ( deltaPos ), Mathf.Cos ( deltaPos ) ) * radius;
    DEBUGdemonstrator2.position = DEBUGdemonstrator.position;
    deltaPos += speed * Time.deltaTime;
  }
}
