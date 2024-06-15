// ENGineering Power Distribution System
public class ENGPDS : IENGConnector {
  public float throughput;

  public override void Pass ( float power ) {
    throughput = power;
    if ( linkA != null ) {
      if ( linkA is ENGPDS ) {
        ( (ENGPDS) linkA ).Pass ( power );
      }
    }
  }
}
