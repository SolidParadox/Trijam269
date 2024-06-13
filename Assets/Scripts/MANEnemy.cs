using UnityEngine;

public class MANEnemy : MANEntity {
  public float lightHP;
  private float originalHP;
  public float shadowDPS;

  public float wakeupDuration;
  public float stunDuration;
  public float stunBulletAddition;
  private float deltaT = 0;

  public int state;

  public LightSensor lightSensor;

  // mapper should be here
  private Vector2 targetHeading;

  public RadarCore radar;
  public RadarCore bulletRadar;

  public GameObject s1, s2;

  public AudioSource slamAS;

  private void Awake () {
    SceneCore.Instance.EnemyAutoSubscribe ( this );
  }

  override protected void Start () {
    base.Start ();
    s1.SetActive ( true );
    s2.SetActive ( false );
    originalHP = lightHP;
  }

  public void SetTarget ( Vector2 target ) {
    targetHeading = target - mika.rgb.position;
    targetHeading.Normalize ();
    mika.rgb.rotation = Vector3.SignedAngle ( Vector3.up, targetHeading, Vector3.forward );
  }

  public float Status () {
    if ( state == 0 ) {
      return 1;
    }
    if ( state == 1 ) {
      return deltaT / wakeupDuration;
    }
    return lightHP / originalHP;
  }

  public bool Feral() {
    return state > 1;
  }

  void Update () {
    if ( state == 0 && !lightSensor.inLight ) {
      deltaT = wakeupDuration;
      state = 1;
    }

    if ( state == 1 && lightSensor.inLight ) {
      state = 0;
    }

    Debug.DrawLine ( mika.rgb.position, mika.rgb.position + targetHeading * 10, Color.magenta );
    if ( state == 2 ) {
      mika.SetThrusterOutput ( targetHeading );
      if ( radar.breached ) {
        state = 3;
        SetTarget ( mika.rgb.position - targetHeading );
        slamAS.Play ();
        deltaT = stunDuration;
      }
    }
    if ( state == 3 ) {
      if ( radar.breached ) {
        mika.SetThrusterOutput ( targetHeading );
      } else {
        mika.rgb.rotation += 0.30f * Random.Range ( 0, 100 );
      }
      if ( bulletRadar.breached ) {
        deltaT += stunBulletAddition;
        for ( int i = 0; i < bulletRadar.contacts.Count; i++ ) {
          Destroy ( bulletRadar.contacts[i] );
        }
      }
    }

    if ( !lightSensor.inLight && state >= 2 ) {
      lightHP -= shadowDPS * Time.deltaTime;
      if ( lightHP <= 0 ) {
        SceneCore.Instance.EnemyFeralDeath ( this );
        Destroy ( gameObject );
      }
    }

    if ( deltaT > 0 ) {
      deltaT -= Time.deltaTime;
      if ( deltaT < 0 ) {
        if ( state == 1 || state == 3 ) {
          if ( state == 1 ) {
            SceneCore.Instance.EnemyWakeUp (); 
            s1.SetActive ( false ); 
            s2.SetActive ( true );
          }
          SetTarget ( SceneCore.Instance.nav.GetPath ( mika.rgb.position, SceneCore.Instance.playerTransform.position ) );
          state = 2;
        }
      }
    }
  }
}
