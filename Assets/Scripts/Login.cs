using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System;

public class Login : MonoBehaviour
{
    public InputField usernameInput;    // champ de texte username
    public InputField passwordInput;    // champ de texte password
    public Image usernameFieldImage;
    public MainMenu menu;
    public GetStatistics stats;
    public GameObject ErrorServeurWindow;
    public GameObject ErrorUserPassword;
    public Button SignInButton;

    public void Start()
    {
        passwordInput.contentType = InputField.ContentType.Password;
        SignInButton.interactable = false;
    }

    public void Update()
    {
        if(usernameInput.text.Length != 0 && passwordInput.text.Length != 0)
        {
            SignInButton.interactable = true;
        }
        else
        {
            SignInButton.interactable = false;
        }
    }

    public void SignInButtonClicked()
    {
        string url = "http://localhost:8080/ws/users/" + usernameInput.text +"/"+ passwordInput.text; // URL de l'endpoint pour récupérer un utilisateur par son ID

        UnityWebRequest request = UnityWebRequest.Get(url);

        var asyncOperation = request.SendWebRequest();
        asyncOperation.completed += OnRequestGetUser;
    }

    public void OnRequestGetUser(AsyncOperation operation)
    {
        UnityWebRequest request = ((UnityWebRequestAsyncOperation)operation).webRequest;

        if (request.result == UnityWebRequest.Result.Success)
        {
            try
            {
                string responseJson = request.downloadHandler.text;
                //UserData user = new UserData(JsonUtility.FromJson<UserData>(responseJson));
                UserData user = JsonConvert.DeserializeObject<UserData>(responseJson);
                Debug.Log("Got User ID :" + user.username);
                Debug.Log("Statistics ID : " + user.statistics.id);

                UserAccess.instance.user = user;
                menu.CloseConnection();
                stats.SetStats();
                menu.connected = true;
            }
            catch (NullReferenceException e)
            {
                ErrorUserPassword.SetActive(true);
                Debug.LogError("Username inexistant: " + e.Message);
            }
            /*catch (UnassignedReferenceException e)
            {
                ErrorUserPassword.SetActive(true);
                Debug.LogError("Username inexistant ou mauvais mot de passe: " + e.Message);
            }*/
        }
        else if(request.result == UnityWebRequest.Result.ConnectionError)
        {
            ErrorServeurWindow.SetActive(true);
            Debug.LogError("Error in connection to the server : " + request.error);
        }
        else
        {
            ErrorUserPassword.SetActive(false);
            Debug.LogError("Error when try to get User : " + request.error);
        }
    }
}
