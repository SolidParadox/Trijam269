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
  private CustomRenderTexture crt;

  public Vector3 deltas;

  public float lightDrain;
  public float lightGain;  // Light Field sample

  private Texture2D deltaLF;

  public Color GetLL ( Vector2 worldPosition ) {
      Vector3 screenPosition = lfCamera.WorldToScreenPoint(new Vector3(worldPosition.x, worldPosition.y, 0));

      int x = Mathf.Clamp(Mathf.RoundToInt(screenPosition.x), 0, lfTexture.width - 1);
      int y = Mathf.Clamp(Mathf.RoundToInt(screenPosition.y), 0, lfTexture.height - 1);

      //Debug.Log ( $"Screen position: {screenPosition}, Texture coordinates: ({x}, {y})" );

      return deltaLF.GetPixel ( x, y );
  }

  void Start () {
    deltaLF = new Texture2D ( lfTexture.width, lfTexture.height, TextureFormat.RGBAFloat, false );
    renderMaterial = new Material ( renderMaterial );
    GetComponent<MeshRenderer>().material = renderMaterial;

    crt = new CustomRenderTexture ( 160, 90, RenderTextureFormat.ARGB32 ) {
      doubleBuffered = true,
      material = new Material ( blendMaterial ),
      updateMode = CustomRenderTextureUpdateMode.OnDemand,
      filterMode = FilterMode.Point
    };

    crt.Create ();
    crt.initializationColor = Color.black;
    crt.Initialize ();
    crt.material.SetTexture ( "_lfSample", lfTexture );
  }

  private void LateUpdate () {
    float timeswitch = Time.smoothDeltaTime;
    if ( crt.material == null ) {
      crt.material = new Material ( blendMaterial );
      crt.material.SetTexture ( "_lfSample", lfTexture );
    }
    crt.material.SetVector ( "_lfDrain", timeswitch * new Vector4 ( deltas.x * lightDrain, deltas.y * lightDrain, deltas.z * lightDrain, 1 ) );
    crt.material.SetFloat ( "_lfGain", timeswitch * lightGain );

    crt.Update ();
    crt.IncrementUpdateCount ();

    renderMaterial.SetTexture ( "_LightField", crt );

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
    if ( crt != null ) {
      crt.Release ();
    }
  }
}
