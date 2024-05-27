using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MANUI : MonoBehaviour {
  public TMP_Text bigMessageLabel;
  public Animator animator;

  private bool playerHasWon = false;
  public int nextLevel = -1;
  public float nextLevelTimer;
  private AsyncOperation asyncOperation; // Reference to the asynchronous operation

  public RectTransform anchorHLG,anchorHLG2;

  public Color eUIDormant;
  public Color eUIFeral;

  void LateUpdate () {
    // Display the game OverScreen via animation
    if ( SceneCore.Instance.gameover ) {
      DisplayBigMessage ( "GAME OVER" );
    }

    int eTC = SceneCore.Instance.feralCount + SceneCore.Instance.dormantCount;

    for ( int i = 0; i < anchorHLG.childCount; i++ ) {
      anchorHLG.GetChild( i ).gameObject.SetActive( i < eTC );
      anchorHLG2.GetChild ( i ).gameObject.SetActive ( i < eTC );
      if ( i < eTC ) {
        anchorHLG.GetChild ( i ).GetComponent<Slider> ().value = 0.6f + SceneCore.Instance.enemies[i].Status ();
        anchorHLG.GetChild ( i ).GetComponent<Slider> ().fillRect.GetComponent<Image>().color = SceneCore.Instance.enemies[i].Feral() ? Color.red : Color.white;
        anchorHLG2.GetChild ( i ).GetComponent<Slider> ().value = 0.6f + SceneCore.Instance.enemies[i].Status ();
        anchorHLG2.GetChild ( i ).GetComponent<Slider> ().fillRect.GetComponent<Image> ().color = SceneCore.Instance.enemies[i].Feral () ? Color.red : Color.white;
      }
    }

    if ( SceneCore.Instance.dormantCount == 0 && SceneCore.Instance.feralCount == 0 && !SceneCore.Instance.gameover && !playerHasWon ) {
      playerHasWon = true;
      asyncOperation = SceneManager.LoadSceneAsync ( nextLevel );
      asyncOperation.allowSceneActivation = false;
    }
    if ( playerHasWon && nextLevel != -1 ) {
      nextLevelTimer -= Time.deltaTime;
      DisplayBigMessage ( "NEXT LEVEL IN: " + nextLevelTimer.ToString ( "F2" ) );
      if ( nextLevelTimer < 0 ) {
        asyncOperation.allowSceneActivation = true;
      }
    }
    if ( Input.GetAxis ( "Reset" ) > 0 ) {
      SceneManager.LoadScene ( SceneManager.GetActiveScene ().buildIndex );
    }
  }

  public void DisplayBigMessage ( string alpha ) {
    bigMessageLabel.text = "\\\\\\ " + alpha + " ///";
    animator.SetBool ( "UP", true );
  }
}
