using UnityEngine;

public abstract class IENGConnector : MonoBehaviour {
  public IENGConnector linkA;
  public abstract void Pass ( float power );
}

public class ENGStation : IENGConnector {
  public float energyOutput;

  private void Update () {
    if ( linkA != null ) {
      linkA.Pass ( energyOutput );
    }
  }

  public override void Pass( float power ) {}
}
