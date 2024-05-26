using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour {
  public ComputeShader fogOfWarCompute;

  public Material mat;
  public RenderTexture capturedTexture, resultTexture;

  public Transform player;

  public float decayFactor;
  public int textureSize = 512;

  private int kernelHandle;
  
  void Start () {
    kernelHandle = fogOfWarCompute.FindKernel ( "CSMain" );
  }

  void Update () {
    // Set shader parameters
    fogOfWarCompute.SetFloat ( "decayFactor", decayFactor );

    // Set the texture as the output target
    fogOfWarCompute.SetTexture ( kernelHandle, "Original", capturedTexture );
    fogOfWarCompute.SetTexture ( kernelHandle, "Result", resultTexture );

    // Dispatch the compute shader
    int threadGroupsX = Mathf.CeilToInt(1366 / 8.0f);
    int threadGroupsY = Mathf.CeilToInt(768 / 8.0f);
    fogOfWarCompute.Dispatch ( kernelHandle, threadGroupsX, threadGroupsY, 1 );
    mat.SetTexture ( "Base", resultTexture );
  }
}
