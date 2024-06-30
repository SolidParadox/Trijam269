using UnityEngine;

public class NULCore : MonoBehaviour {
  public static NULCore Instance { get; private set; }
  public Camera lfCamera;

  private void Awake () {
    if ( Instance == null ) {
      Instance = this;
    } else {
      Destroy ( gameObject );
    }
  }

  public Material blendMaterial;

  public RenderTexture lfTexture;  // Light Field

  public Material renderMaterial;
  public CustomRenderTexture crt;

  public Vector3 deltas;

  public float strengthDrain;
  public float strengthLFS;  // Light Field sample

  private Texture2D deltaLF;

  public Color GetLL ( Vector2 worldPosition ) {
      Vector3 screenPosition = lfCamera.WorldToScreenPoint(new Vector3(worldPosition.x, worldPosition.y, 0));

      int x = Mathf.Clamp(Mathf.RoundToInt(screenPosition.x), 0, lfTexture.width - 1);
      int y = Mathf.Clamp(Mathf.RoundToInt(screenPosition.y), 0, lfTexture.height - 1);

      //Debug.Log ( $"Screen position: {screenPosition}, Texture coordinates: ({x}, {y})" );

      return deltaLF.GetPixel ( x, y );
  }

  void Start () {
    deltaLF = new Texture2D ( lfTexture.width, lfTexture.height, TextureFormat.RGB24, false );
    renderMaterial = new Material ( renderMaterial );
    blendMaterial.SetTexture ( "_LFSample", lfTexture );
    crt.Create ();
  }

  void Update () {
    blendMaterial.SetVector ( "_DimColor", 1 * new Vector4 ( deltas.x, deltas.y, deltas.z, 1 ) * strengthDrain );
    blendMaterial.SetFloat ( "_LfPower", 1 * strengthLFS );

    crt.Initialize ();
    crt.Update ();

    renderMaterial.SetTexture ( "_LightField", crt );
  }

  private void LateUpdate () {
    RenderTexture currentActiveRT = RenderTexture.active;
    RenderTexture.active = lfTexture;

    deltaLF.ReadPixels ( new Rect ( 0, 0, lfTexture.width, lfTexture.height ), 0, 0 );
    deltaLF.Apply ();

    RenderTexture.active = currentActiveRT;
  }

  private void OnDestroy () {
    if ( deltaLF != null ) {
      Destroy ( deltaLF );
    }
  }
}
