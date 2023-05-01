using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Login : MonoBehaviour
{
    public string SceneToLoad;  //nom de la sc�ne de jeu
    public InputField usernameInput;    // champ de texte username
    public InputField passwordInput;    // champ de texte password
    public GameObject settingsWindow;   //fenetre des options

    IEnumerator LoginWithInputField()
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", usernameInput.text);
        form.AddField("loginPass", passwordInput.text);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackend/unity_back_php/Login.php", form))
        {
            yield return www.SendWebRequest();
            if(www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                SceneManager.LoadScene(SceneToLoad);
            }
        }
    }


    public void Quit()          //ferme le jeu
    {
        Application.Quit();
    }

    public void Settings()      //ouvre la fenetre des options
    {
        settingsWindow.SetActive(true);
    }

    public void CloseSettings() //ferme la fenetre des options
    {
        settingsWindow.SetActive(false);
    }

}
