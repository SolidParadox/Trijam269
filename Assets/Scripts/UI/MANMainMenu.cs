using UnityEngine.SceneManagement;
using UnityEngine;

public class MANMainMenu : MonoBehaviour {

  private AsyncOperation asyncOperation; // Reference to the asynchronous operation

  public void ButtonPlay () {
    asyncOperation = SceneManager.LoadSceneAsync ( 1 );
    asyncOperation.allowSceneActivation = true;
  }
}
