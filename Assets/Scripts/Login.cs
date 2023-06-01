using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Login : MonoBehaviour
{
    public InputField usernameInput;    // champ de texte username
    public InputField passwordInput;    // champ de texte password
    public MainMenu menu;

    public void Start()
    {

        passwordInput.contentType = InputField.ContentType.Password;
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
            string responseJson = request.downloadHandler.text;
            UserData user = new UserData(JsonUtility.FromJson<UserData>(responseJson));
            Debug.Log("Got User ID :" + user.Username);
            Debug.Log("Statistics ID : " + user.Statistics.Id);

            UserAccess.instance.user = user;
            menu.CloseConnection();
            menu.connected = true;
        }
        else
        {
            Debug.LogError("Error when try to get User : " + request.error);
        }
    }
}
