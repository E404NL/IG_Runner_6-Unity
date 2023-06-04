using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
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
    public InputField codePostalField;
    public Image usernameFieldImage;
    public Image passwordFieldImage;
    public Image passwordConfirmationImage;
    public Image emailFieldImage;
    public Image codePostalFieldImage;
    public Image ageFieldImage;
    public Text UsernameText;
    public Text PasswordText;
    public Text PasswordConfirmationText;
    public Text EmailText;
    public Text AgeText;
    public Text CodePostalText;
    public Toggle Male;
    public Toggle Female;
    public Toggle Other;
    public Button SignUp;
    public MainMenu menu;
    public GetStatistics stats;

    private Dictionary<string,bool> isgoods = new Dictionary<string,bool>();
    private string genre = "";
    

    private const string serverURL = "http://localhost:8080/ws/users";

    private void Start()
    {
        isgoods["Username"] = false;
        isgoods["Password"] = false;    //false
        isgoods["PasswordConfirmation"] = false;
        isgoods["Email"] = false;
        isgoods["Name"] = false;
        isgoods["FirstName"] = false;
        isgoods["Age"] = false;
        isgoods["Genre"] = false;
        isgoods["PostalCode"] = false;

        emailField.onEndEdit.AddListener(CheckEmailAvailability);
        passwordField.contentType = InputField.ContentType.Password;
        passwordField.onValueChange.AddListener(verifyPasswordLevel);
        passwordConfirmationField.contentType = InputField.ContentType.Password;
        passwordConfirmationField.onEndEdit.AddListener(verifySamePassWord);
        usernameField.onEndEdit.AddListener(CheckUsernameAvailability);
        codePostalField.onValueChanged.AddListener(checkPostalCode);
        ageField.onEndEdit.AddListener(checkAge);
        Male.onValueChanged.AddListener(checkGenre);
        Female.onValueChanged.AddListener(checkGenre);
        Other.onValueChanged.AddListener(checkGenre);
        nameField.onEndEdit.AddListener(checkName);
        firstnameField.onEndEdit.AddListener(checkFirstName);
    }

    private void Update()
    {
        SignUp.interactable = CheckIfCanSignUp();
    }

    private void OnDisable()
    {
        passwordField.onEndEdit.RemoveAllListeners();
        emailField.onEndEdit.RemoveListener(CheckEmailAvailability);
        usernameField.onEndEdit.RemoveListener(CheckUsernameAvailability);
        passwordConfirmationField.onEndEdit.RemoveListener(verifySamePassWord);
        codePostalField.onEndEdit.RemoveListener(checkPostalCode);
        ageField.onEndEdit.RemoveListener(checkAge);
        Male.onValueChanged.RemoveListener(checkGenre);
        Female.onValueChanged.RemoveListener(checkGenre);
        Other.onValueChanged.RemoveListener(checkGenre);
        nameField.onEndEdit.RemoveListener(checkName);
        firstnameField.onEndEdit.RemoveListener(checkFirstName);
    }


    public void verifySamePassWord(string password)
    {
        if(passwordField.text == passwordConfirmationField.text)
        {
            passwordConfirmationImage.color = Color.white;
            PasswordConfirmationText.text = "";
            isgoods["PasswordConfirmation"] = true;
            PrintIsGood();
        }
        else
        {
            passwordConfirmationImage.color = Color.red;
            PasswordConfirmationText.text = "Mot de passe différent !"; 
            isgoods["PasswordConfirmation"] = false;
            PrintIsGood();
        }
    }

    public void verifyPasswordLevel(string password)
    {

        Regex hasNumber = new Regex(@"[0-9]+");
        Regex hasUpperChar = new Regex(@"[A-Z]+");
        Regex hasMinimum8Chars = new Regex(@".{8,}");
        if(hasNumber.IsMatch(passwordField.text) && hasUpperChar.IsMatch(passwordField.text) && hasMinimum8Chars.IsMatch(passwordField.text))
        {
            Debug.Log("Match !");
            passwordFieldImage.color = Color.white;
            PasswordText.text = "";
            isgoods["Password"] = true;
            //PrintIsGood();
        }
        else
        {
            Debug.Log("Pas match !");
            PasswordText.text = "Mot de passe faible : nécessite 8 caractères, une majuscule, une minuscule, un chiffre et un caractère spécial !";
            isgoods["Password"] = false;
            passwordFieldImage.color = Color.red;
            //PrintIsGood();
        }
    }

    public void checkPostalCode(string code)
    {
        if(codePostalField.text.Length != 5)
        {
            codePostalFieldImage.color = Color.red;
            CodePostalText.text = "Merci de mettre un code postal à 5 chiffres !";
            isgoods["PostalCode"] = false;
            PrintIsGood();
        }
        else
        {
            codePostalFieldImage.color = Color.white;
            CodePostalText.text = "";
            isgoods["PostalCode"] = true;
            PrintIsGood();
        }
    }

    public void checkName(string name)
    {
        Regex regex = new Regex("^[A-Z][a-zA-Z]*$");
        if (regex.IsMatch(nameField.text))
        {
            isgoods["Name"] = true;
            PrintIsGood();
        }
        else
        {
            isgoods["Name"] = false;
            PrintIsGood();
        }
    }

    public void checkFirstName(string firstname)
    {
        Regex regex = new Regex("^[A-Z][a-zA-Z]*$");
        if (regex.IsMatch(nameField.text))
        {
            isgoods["FirstName"] = true;
            PrintIsGood();
        }
        else
        {
            isgoods["FirstName"] = false;
            PrintIsGood();
        }
    }

    public void checkAge(string age)
    {
        Regex regex = new Regex("^(1[3-9]|[2-8][0-9]|9[0-9])$");
        if (regex.IsMatch(ageField.text) && (int.Parse(ageField.text) > 99 || int.Parse(ageField.text) < 13))
        {
            ageFieldImage.color = Color.red;
            AgeText.text = "Merci d'entrer un âge compris entre 13 et 99 ans!";
            isgoods["Age"] = false;
            PrintIsGood();

        }
        else
        {
            ageFieldImage.color = Color.white;
            AgeText.text = "";
            isgoods["Age"] = true;
            PrintIsGood();

        }
    }

    public void checkGenre(bool isOn)
    {
        if(!Male.isOn && !Female.isOn && !Other.isOn)
        {
            isgoods["Genre"] = false;
            PrintIsGood();
        }
        if(Male.isOn)
        {
            Female.isOn = false;
            Other.isOn = false;
            genre = "Masculin";
            isgoods["Genre"] = true;
            PrintIsGood();
        }
        if (Female.isOn)
        {
            Male.isOn = false;
            Other.isOn = false;
            genre = "Féminin";
            isgoods["Genre"] = true;
            PrintIsGood();
        }
        if (Other.isOn)
        {
            Female.isOn = false;
            Male.isOn = false;
            genre = "Autre";
            isgoods["Genre"] = true;
            PrintIsGood();
        }
    }

    public void CheckUsernameAvailability(string username)
    {
        if(usernameField.text == "")
        {
            UsernameText.text = "Merci d'entrer un pseudo !";
            return;
        }
        UsernameText.text = "";
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
                UsernameText.text = "Ce pseudo est déjà pris!";
                isgoods["Username"] = false;
                PrintIsGood();
            }
            else
            {
                Debug.Log("This username's already free.");
                usernameFieldImage.color = Color.white;
                UsernameText.text = "";
                isgoods["Username"] = true;
                PrintIsGood();
            }
        }
        else
        {
            Debug.LogError("Error in verifying username's already exist in DB : " + request.error);
        }
    }
    public void CheckEmailAvailability(string email)
    {
        Regex regex = new Regex("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.+[a-zA-Z]{2,}$");
        if (!regex.IsMatch(emailField.text))
        {
            emailFieldImage.color = Color.red;
            EmailText.text = "Merci d'entrer une email valide !";
            isgoods["Email"] = false;
            PrintIsGood();
            return;
        }
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
                EmailText.text = "Cet adresse email est déjà prise !";
                isgoods["Email"] = false;
                PrintIsGood();
            }
            else
            {
                Debug.Log("This email's already free.");
                emailFieldImage.color = Color.white;
                EmailText.text = "";
                isgoods["Email"] = true;
                PrintIsGood();
            }
        }
        else
        {
            Debug.LogError("Error in verifying email's already exist in DB : " + request.error);
        }
    }

    public bool CheckIfCanSignUp()
    {
        bool ok = false;
        foreach (bool value in isgoods.Values)
        {
            if (!value)
            {
                return false;
            }
        }
        return true;
    }

    public void PrintIsGood()
    {
        foreach (bool value in isgoods.Values)
        {
            Debug.Log(value);
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
            genre,
            int.Parse(codePostalField.text)
        );
        Debug.Log(user);
        // Convertir l'objet User en JSON
        string jsonData = JsonConvert.SerializeObject(user);
        Debug.Log(jsonData);

        // Création de la requête POST avec le contenu JSON
        UnityWebRequest request = UnityWebRequest.Post(serverURL, jsonData);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");


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
        if (request.result == UnityWebRequest.Result.Success)
        {
            string responseJson = request.downloadHandler.text;
            //UserData user = new UserData(JsonUtility.FromJson<UserData>(responseJson));
            UserData user = JsonConvert.DeserializeObject<UserData>(responseJson);
            Debug.Log("Got User ID :" + user.username);
            Debug.Log("Statistics ID : " + user.statistics.id);

            UserAccess.instance.user = user;
            menu.CloseSignUp();
            stats.SetStats();
            menu.connected = true;
        }
        else
        {
            Debug.LogError("Error when try to get User : " + request.error);
        }
    }
}
