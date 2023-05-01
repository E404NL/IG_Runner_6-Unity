using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Web : MonoBehaviour
{

    // public InputField usernameInput;
    // public InputField passwordInput;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetTimer());
        //StartCoroutine(GetUsers());
        //StartCoroutine(Login("TheDev", "uchtxc!"));
        //StartCoroutine(RegisterUser("FirstRegister", "bw4bw0hbwIh", "zalut.zava@gmou.con", "Prenom", "nom"));
        //startCoroutine(LoginWithInputField();)
    }

    IEnumerator GetTimer()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost/UnityBackend/unity_back_php/GetDate.php")) {
            yield return www.Send();

            if(www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                byte[] results = www.downloadHandler.data;
            }
        }
    }

    IEnumerator GetUsers()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost/UnityBackend/unity_back_php/GetUsers.php"))
        {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                byte[] results = www.downloadHandler.data;
            }
        }
    }

    IEnumerator Login(string _username, string _password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", _username);
        form.AddField("loginPass", _password);

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
            }
        }
    }

    // IEnumerator LoginWithInputField()
    // {
    //     WWWForm form = new WWWForm();
    //     form.AddField("loginUser", usernameInput.text);
    //     form.AddField("loginPass", passwordInput.text);

    //     using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackend/unity_back_php/Login.php", form))
    //     {
    //         yield return www.SendWebRequest();
    //         if(www.isNetworkError || www.isHttpError)
    //         {
    //             Debug.Log(www.error);
    //         }
    //         else
    //         {
    //             Debug.Log(www.downloadHandler.text);
    //         }
    //     }
    // }

    IEnumerator RegisterUser(string username, string password, string email, string firstName, string lastName)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);
        form.AddField("loginEmail", email);
        form.AddField("loginFirstName", firstName);
        form.AddField("loginLastName", lastName);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackend/unity_back_php/RegisterUser.php", form))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
