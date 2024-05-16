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

  public RectTransform anchorHLG;

  public Color eUIDormant;
  public Color eUIFeral;

  void LateUpdate () {
    // Display the game OverScreen via animation
    if ( SceneCore.Instance.gameover ) {
      DisplayBigMessage ( "GAME OVER" );
    }

    int eTC = SceneCore.Instance.feralCount + SceneCore.Instance.dormantCount;

    Vector2 hlgSizeDelta = anchorHLG.sizeDelta;
    hlgSizeDelta.x = eTC * 30 + ( eTC - 1 ) * 5;
    anchorHLG.sizeDelta = hlgSizeDelta;

    for ( int i = 0; i < anchorHLG.childCount; i++ ) {
      anchorHLG.GetChild( i ).gameObject.SetActive( i < eTC );
      anchorHLG.GetChild ( i ).GetComponent<Image> ().color = i < SceneCore.Instance.dormantCount ? eUIDormant : eUIFeral;
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
  IEnumerator SwitchToLoadedSceneCoroutine () {
    // Wait for the scene loading operation to complete
    while ( !asyncOperation.isDone ) {
      yield return null;
    }

    // Wait for the delay before switching to the loaded scene
    yield return new WaitForSeconds ( nextLevelTimer );

    // Allow the loaded scene to be activated
    asyncOperation.allowSceneActivation = true;
  }

  public void DisplayBigMessage ( string alpha ) {
    bigMessageLabel.text = "\\\\\\ " + alpha + " ///";
    animator.SetBool ( "UP", true );
  }
}
