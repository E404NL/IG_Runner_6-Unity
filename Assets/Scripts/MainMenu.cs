using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string LevelToLoad;  //nom de la sc�ne de jeu
    public bool connected = false;  //connecté ? Non connecté par défaut;

    public GameObject settingsWindow;   //fenetre des options
    public GameObject creditsWindow;    //fenetre des credits
    public GameObject tutorialWindow;   //fenetre du tutoriel
    public GameObject connectionWindow;  //fenetre de connexion
    public GameObject signUpWindow;     //fenetre de creation de compte

    public GameObject playButton;       //game launch button
    public GameObject signInButton;     //sign in button
    public GameObject signUpButton;     //sign up button
    public GameObject disconnectButton; //disconnect button

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
        }
        else
        {
            playButton.SetActive(true);
            signInButton.SetActive(false);
            signUpButton.SetActive(false);
            disconnectButton.SetActive(true);
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

    public void DisconnectionButton()
    {
        this.connected = false;
    }
}
