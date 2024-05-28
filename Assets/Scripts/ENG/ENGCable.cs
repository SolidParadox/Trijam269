using UnityEngine;

public class ENGCable : IENGConnector {
  public float throughput;

  public override void Pass ( float power ) {
    throughput = power;
    if ( linkA != null ) {
      if ( linkA is ENGCable ) {
        ( (ENGCable) linkA ).Pass ( power );
      }
    }
  }
}
