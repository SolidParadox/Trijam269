using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCore : MonoBehaviour {
  public static SceneCore Instance { get; private set; }

  public Transform playerTransform;
  public Transform spawnTransform;

  public int dormantCount = 0;
  public int feralCount = 0;

  public bool gameover;

  private void Awake () {
    // Ensure only one instance of the class exists
    if ( Instance == null ) {
      Instance = this;
    } else {
      Destroy ( gameObject );
    }
  }
  public void EnemyAutoSubscribe () {
    dormantCount++;
  }

  public void EnemyWakeUp () {
    dormantCount--;
    feralCount++;
  }
  public void EnemyFeralDeath () {
    feralCount--;
  }
}
