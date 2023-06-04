using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string LevelToLoad;
    public bool connected = false;  //connecté ? Non connecté par défaut;

    public GameObject settingsWindow;
    public GameObject creditsWindow;
    public GameObject tutorialWindow;
    public GameObject connectionWindow;
    public GameObject signUpWindow;
    public GameObject statisticsWindow;

    public GameObject playButton;
    public GameObject signInButton;
    public GameObject signUpButton;
    public GameObject disconnectButton;
    public GameObject statisticsButton;
    private void Start()
    {

    }

    private void Update()
    {
        if (!connected)
        {
            playButton.SetActive(false);
            signInButton.SetActive(true);
            signUpButton.SetActive(true);
            disconnectButton.SetActive(false);
            statisticsButton.SetActive(false);
        }
        else
        {
            playButton.SetActive(true);
            signInButton.SetActive(false);
            signUpButton.SetActive(false);
            disconnectButton.SetActive(true);
            statisticsButton.SetActive(true);
        }
    }

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

    public void Connexion()
    {
        connectionWindow.SetActive(true);
    }

    public void CloseConnection()
    {
        connectionWindow.SetActive(false);
    }

    public void SignUp()
    {
        signUpWindow.SetActive(true);
    }

    public void CloseSignUp()
    {
        signUpWindow.SetActive(false);
    }

    public void Statistics()
    {
        statisticsWindow.SetActive(true);
    }

    public void CloseStatistics()
    {
        statisticsWindow.SetActive(false);
    }

    public void DisconnectionButton()
    {
        UserAccess.instance.user = null;
        this.connected = false;
    }
}
