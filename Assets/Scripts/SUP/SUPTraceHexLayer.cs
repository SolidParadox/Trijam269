using UnityEngine;

public class SUPTraceHexLayer : MonoBehaviour {
  public GameObject traceObject;
  public Vector2Int dimensions;
  public float scale;

  public void Place () {
    while ( transform.childCount < dimensions.x * dimensions.y ) {
      Instantiate ( traceObject, transform );
    }
    float ypos = 0;
    for ( int i = 0; i < dimensions.x; i++ ) {
      for ( int j = 0; j < dimensions.y; j++ ) {
        Debug.Log ( i + " " + j + " " + " " );
        transform.GetChild ( i + j * dimensions.x ).position = new Vector3 ( scale * i + ( j % 2 == 0 ? scale / 2 : 0 ), ypos );
        ypos += j % 2 == 0 ? scale : scale * 0.866f;
      }
      ypos = 0;
    }
  }
}
