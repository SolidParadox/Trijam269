using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENGSplitter : IENGConnector {
  public IENGConnector[] connectors;
  public float throughput;

  public override void Pass ( float power ) {
    throughput = power;
    for ( int i = 0; i < connectors.Length; i++ ) {
      connectors [ i ].Pass ( throughput );
    }
  }
}
