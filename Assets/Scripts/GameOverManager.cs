
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;

    public static GameOverManager instance;
  private void Awake()
  {
    if (instance !=null)
    {
      Debug.LogWarning("Il y a plus d'une instance de GameOverManager dnas la scéne");
      return;
    }
    instance = this;
  }

    public void OnPlayerDeath()
    {
      if(CurrentSceneManager.instance.isPlayerPresentByDefault)
      {
        DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
      }
        gameOverUI.SetActive(true);
    }

//recommencer le niveau
    public void RetryButton()
    {
    //recharger la scene
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    
    PlayerHealth.instance.Respawn();
       // reactiver les mouvements du joueurs et lui rendre sa vie
    gameOverUI.SetActive(false);

    }

    //fermer le jeu
    public void QuitButton()
    {
        Application.Quit();
    }
  
}
