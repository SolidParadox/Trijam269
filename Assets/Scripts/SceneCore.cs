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

  public List<MANEnemy> enemies;

  private void Awake () {
    // Ensure only one instance of the class exists
    if ( Instance == null ) {
      Instance = this;
      enemies = new List<MANEnemy>();
    } else {
      Destroy ( gameObject );
    }
  }
  public void EnemyAutoSubscribe ( MANEnemy instance ) {
    dormantCount++;
    if ( !enemies.Contains ( instance ) ) {
      enemies.Add ( instance );
    }
  }

  public void EnemyWakeUp () {
    dormantCount--;
    feralCount++;
  }
  public void EnemyFeralDeath ( MANEnemy instance ) {
    feralCount--;
    if ( enemies.Contains ( instance ) ) {
      enemies.Remove ( instance );
    }
  }
}
