using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{

    public GameObject gameOverUI;   //GameOverMenu
    public static GameOverManager instance; //instance
    public Button RetryButton;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de GameOverManager dans la scene");
            return;
        }
        instance = this;
    }

    public void OnPlayerDeath()
    {
        gameOverUI.SetActive(true);
    }

    public void Retry()     //rechargement de la scène
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void MainMenu()  //retour au menu principal
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()      //Quitte le jeu
    {
        Application.Quit();
    }
}
