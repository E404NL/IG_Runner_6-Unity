using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.IO;


public class CreateAccount : MonoBehaviour
{
    public InputField usernameField;
    public InputField passwordField;
    public InputField passwordConfirmationField;
    public InputField emailField;
    public InputField nameField;
    public InputField firstnameField;
    public InputField ageField;
    public InputField sexField;
    public InputField codePostalField;
    public Image usernameFieldImage;
    public Image passwordFieldImage;
    public Image passwordConfirmationImage;
    public Image emailFieldImage;

    private const string serverURL = "http://localhost:8080/ws/users";

    private void Start()
    {
        emailField.onEndEdit.AddListener(CheckEmailAvailability);
        passwordField.contentType = InputField.ContentType.Password;
        passwordConfirmationField.contentType = InputField.ContentType.Password;
        usernameField.onEndEdit.AddListener(CheckUsernameAvailability);
        passwordConfirmationField.onEndEdit.AddListener(verifyPassWord);
    }

    private void Update()
    {
        
    }

    private void OnDisable()
    {
        emailField.onEndEdit.RemoveListener(CheckEmailAvailability);
        usernameField.onEndEdit.RemoveListener(CheckUsernameAvailability);
        passwordConfirmationField.onEndEdit.RemoveListener(verifyPassWord);
    }



    public void verifyPassWord(string password)
    {
        if(passwordField.text == passwordConfirmationField.text)
        {
            passwordConfirmationImage.color = Color.green;
            passwordFieldImage.color = Color.green;
        }
        else
        {
            passwordConfirmationImage.color = Color.red;
            passwordFieldImage.color = Color.red;
        }
    }

    public void CheckUsernameAvailability(string username)
    {
        // URL de l'endpoint pour vérifier l'existence d'un utilisateur
        string url = serverURL + "/check-username/" + usernameField.text;
        UnityWebRequest request = UnityWebRequest.Get(url);

        var asyncOperation = request.SendWebRequest();
        asyncOperation.completed += OnRequestTestUsernameCompleted;

    }

    private void OnRequestTestUsernameCompleted(AsyncOperation operation)
    {
        UnityWebRequest request = ((UnityWebRequestAsyncOperation)operation).webRequest;

        if (request.result == UnityWebRequest.Result.Success)
        {
            string responseJson = request.downloadHandler.text;
            bool exists = bool.Parse(responseJson);

            if (exists)
            {
                Debug.Log("This username's already taken!");
                usernameFieldImage.color = Color.red;
            }
            else
            {
                Debug.Log("This username's already free.");
                usernameFieldImage.color = Color.green;
            }
        }
        else
        {
            Debug.LogError("Error in verifying username's already exist in DB : " + request.error);
        }
    }
    public void CheckEmailAvailability(string email)
    {
        // URL de l'endpoint pour vérifier l'existence d'un utilisateur
        string url = serverURL + "/check-email/" + emailField.text;
        UnityWebRequest request = UnityWebRequest.Get(url);

        var asyncOperation = request.SendWebRequest();
        asyncOperation.completed += OnRequestTestEmailCompleted;

    }
    private void OnRequestTestEmailCompleted(AsyncOperation operation)
    {
        UnityWebRequest request = ((UnityWebRequestAsyncOperation)operation).webRequest;

        if (request.result == UnityWebRequest.Result.Success)
        {
            string responseJson = request.downloadHandler.text;
            bool exists = bool.Parse(responseJson);

            if (exists)
            {
                Debug.Log("This email's already taken!");
                emailFieldImage.color = Color.red;
            }
            else
            {
                Debug.Log("This email's already free.");
                emailFieldImage.color = Color.green;
            }
        }
        else
        {
            Debug.LogError("Error in verifying email's already exist in DB : " + request.error);
        }
    }

    public void SignUpButtonClicked()
    {
        // Création de l'objet User avec les données du formulaire
        UserData user = new UserData(
            usernameField.text,
            passwordField.text,
            emailField.text,
            nameField.text,
            firstnameField.text,
            int.Parse(ageField.text),
            sexField.text,
            int.Parse(codePostalField.text)
        );

        // Convertir l'objet User en JSON
        string jsonData = JsonUtility.ToJson(user);

        // Création de la requête POST avec le contenu JSON
        UnityWebRequest request = UnityWebRequest.Post(serverURL, jsonData);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Yeet la request
        var asyncOperation = request.SendWebRequest();
        asyncOperation.completed += OnRequestCompleted;
    }

    private void OnRequestCompleted(AsyncOperation operation)
    {
        UnityWebRequest request = ((UnityWebRequestAsyncOperation)operation).webRequest;

        if (request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error in request : " + request.error +".");
        }
        else
        {
            Debug.Log("Successfull sent request !");
        }
    }
}
