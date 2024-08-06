using UnityEngine;

public class PUCKTerminal : MonoBehaviour {
  public Animator animator;

  public PUCK puck;

  // Could be made to work with signals instead, would make this terribly more easy
  public float spawnTime;
  private float timerSpawn;

  public float launchVelocity;
  public float launchAngularVelocity;

  private void Update () {
    if ( Input.GetKeyDown( KeyCode.R ) ) {
      puck.StartStun ();
      puck.powerTrain.rgb.position = transform.position;
      //puck.powerTrain.rgb.rotation = 0;

      puck.powerTrain.rgb.velocity = Vector2.zero;
      puck.powerTrain.rgb.constraints = RigidbodyConstraints2D.FreezeRotation;
      
      puck.powerTrain.enabled = false;
      puck.flashlight.enabled = false;
      
      timerSpawn = spawnTime;
    }
    if ( timerSpawn > 0 ) {
      timerSpawn -= Time.deltaTime;
      if ( timerSpawn <= 0 ) {
        puck.StartStun ();
        puck.powerTrain.rgb.constraints = RigidbodyConstraints2D.None;

        puck.powerTrain.rgb.velocity = transform.up * launchVelocity;
        puck.powerTrain.rgb.angularVelocity = launchAngularVelocity;

        puck.powerTrain.enabled = true;
        puck.flashlight.enabled = true;
        puck.powerTrain.SetThrusterOutput ( Vector2.zero );

        timerSpawn = 0;
      }
    }
  }
}
