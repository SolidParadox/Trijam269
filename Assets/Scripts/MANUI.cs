using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MANUI : MonoBehaviour {
  public TMP_Text bigMessageLabel;
  public Animator animator;

  private bool playerHasWon = false;
  public int nextLevel = -1;
  public float nextLevelTimer;
  private AsyncOperation asyncOperation; // Reference to the asynchronous operation

  public RectTransform anchorHLG;

  public Animator cutterIndicator;
  public RadarCore cutterRadar;

  private void Start () {
    int eTC = SceneCore.Instance.feralCount + SceneCore.Instance.dormantCount;

    for ( int i = 0; i < anchorHLG.childCount; i++ ) {
      anchorHLG.GetChild ( i ).gameObject.SetActive ( i < eTC );
      if ( i < eTC ) {
        anchorHLG.GetChild ( i ).GetComponent<CUEuler> ().target = SceneCore.Instance.enemies[i];
      }
    }
    cutterRadar = SceneCore.Instance.playerTransform.GetChild ( 3 ).GetComponent<RadarCore>();
  }

  void LateUpdate () {
    // Display the game OverScreen via animation
    if ( SceneCore.Instance.gameover ) {
      DisplayBigMessage ( "GAME OVER" );
    }

    if ( SceneCore.Instance.dormantCount == 0 && SceneCore.Instance.feralCount == 0 && !SceneCore.Instance.gameover && !playerHasWon ) {
      playerHasWon = true;
      asyncOperation = SceneManager.LoadSceneAsync ( nextLevel );
      asyncOperation.allowSceneActivation = false;
    }
    if ( playerHasWon && nextLevel != -1 ) {
      nextLevelTimer -= Time.deltaTime;
      DisplayBigMessage ( "EXTRACTION IN : " + nextLevelTimer.ToString ( "F2" ) );
      if ( nextLevelTimer < 0 ) {
        asyncOperation.allowSceneActivation = true;
      }
    }
    if ( Input.GetAxis ( "Reset" ) > 0 ) {
      SceneManager.LoadScene ( SceneManager.GetActiveScene ().buildIndex );
    }
    cutterIndicator.SetBool ( "CableNearby", cutterRadar.breached );
  }

  public void DisplayBigMessage ( string alpha ) {
    bigMessageLabel.text = "\\\\\\ " + alpha + " ///";
    animator.SetBool ( "UP", true );
  }
}
