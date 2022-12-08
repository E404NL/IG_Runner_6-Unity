using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string LevelToLoad;  //nom de la scène de jeu

    public GameObject settingsWindow;   //fenetre des options
    public GameObject creditsWindow;    //fenetre des credits
    public GameObject tutorialWindow;   //fenetre du tutoriel

    public void StartGame()     //charge la scene du jeu
    {
        SceneManager.LoadScene(LevelToLoad);
    }
    public void Settings()      //ouvre la fenetre des options
    {
        settingsWindow.SetActive(true);
    }

    public void CloseSettings() //ferme la fenetre des options
    {
        settingsWindow.SetActive(false);
    }

    public void Quit()          //ferme le jeu
    {
        Application.Quit();
    }

    public void Credits()       //ouvre la fenetre des credits
    {
        creditsWindow.SetActive(true);
    }

    public void CloseCredits()  //ferme la fenetre des credits
    {
        creditsWindow.SetActive(false);
    }


    public void Tutorial()  //ouvre la fenetre du tutoriel
    {
        tutorialWindow.SetActive(true);
    }

    public void CloseTutorial() //ferme la fenetre du tutoriel
    {
        tutorialWindow.SetActive(false);
    }
}
