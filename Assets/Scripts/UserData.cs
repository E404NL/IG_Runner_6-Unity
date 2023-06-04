//Object of this class will hold the data
//this object will be converted to JSON

using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;


[System.Serializable]
public class UserData
{
    private long _id;
    private string _username;
    private string _password;
    private string _email;
    private string _name;
    private string _firstname;
    private int _age;
    private string _gender;
    private int _codePostal;
    private long _rank;
    private int _tryCounter;
    private string _actualSkin;
    private int _totalScore;
    private StatisticsData _statistics;
    private GrindData _grind;
    /*public SuccessData[] _successes;*/

    public UserData()
    {

    }

    public UserData(string username, string password, string email,
            string name, string firstname, int age, string gender, int codePostal)
    {
        this.username = username;
        this.password = password;
        this.email = email;
        this.name = name;
        this.firstname = firstname;
        this.age = age;
        this.gender = gender;
        this.codePostal = codePostal;
        this.rank = 0;
        this.tryCounter = 3;
        this.actualSkin = "default";
        this.totalScore = 0;
        this.statistics = new StatisticsData();
        this.grind = new GrindData();
    }

    public UserData(UserData user)
    {
        this.id = user.id;
        this.username = user.username;
        this.password = user.password;
        this.email = user.email;
        this.name = user.name;
        this.firstname = user.firstname;
        this.age = user.age;
        this.gender = user.gender;
        this.codePostal = user.codePostal;
        this.rank = user.rank;
        this.tryCounter = user.tryCounter;
        this.actualSkin = user.actualSkin;
        this.totalScore = user.totalScore;
        this.statistics = user.statistics;
        this.grind = user.grind;
    }

    public long id
    {
        get => _id;
        set => _id = value;
    }

    public string username
    {
        get => _username;
        set => _username = value;
    }

    public string password
    {
        get => _password;
        set => _password = value;
    }

    public string email
    {
        get => _email;
        set => _email = value;
    }

    public string name
    {
        get => _name;
        set => _name = value;
    }

    public string firstname
    {
        get => _firstname;
        set => _firstname = value;
    }

    public int age
    {
        get => _age;
        set => _age = value;
    }

    public string gender
    {
        get => _gender;
        set => _gender = value;
    }

    public int codePostal
    {
        get => _codePostal;
        set => _codePostal = value;
    }

    public long rank
    {
        get => _rank;
        set => _rank = value;
    }

    public int tryCounter
    {
        get => _tryCounter;
        set => _tryCounter = value;
    }

    public string actualSkin
    {
        get => _actualSkin;
        set => _actualSkin = value;
    }

    public int totalScore
    {
        get => _totalScore;
        set => _totalScore = value;
    }

    public StatisticsData statistics
    {
        get => _statistics;
        set => _statistics = value;
    }

    public GrindData grind
    {
        get => _grind;
        set => _grind = value;
    }

    public void UpdateUser()
    {
        string url = "http://localhost:8080/ws/users/" + this.id; // URL de l'endpoint pour récupérer un utilisateur par son ID
        string jsonData = JsonConvert.SerializeObject(this);

        UnityWebRequest request = UnityWebRequest.Put(url, jsonData);

        var asyncOperation = request.SendWebRequest();
        asyncOperation.completed += OnRequestPutUser;
    }

    public void OnRequestPutUser(AsyncOperation operation)
    {
        UnityWebRequest request = ((UnityWebRequestAsyncOperation)operation).webRequest;

        if (request.result == UnityWebRequest.Result.Success)
        {
            string responseJson = request.downloadHandler.text;
            string json = JsonUtility.ToJson(this);
            Debug.Log("Put User :" + this.username);
        }
        else
        {
            Debug.LogError("Error when try to put User : " + request.error);
        }
    }

    public void UpdateTotalScore()
    {
        StatisticsData stats = this.statistics;
        GrindData grind = this.grind;
        int success_points = grind.successPoints;
        float dist = stats.totalDistance;
        int coins = stats.totalCoins;
        int totalScore = this.totalScore;
        if (success_points != 0)
        {
            totalScore = (int)(((float)success_points * 0.015f) * (dist + coins) + dist + coins);
            this.totalScore = ((int)totalScore);
        }
        else
        {
            totalScore = (int)dist + coins;
            this.totalScore = ((int)totalScore);
        }
    }

    public void UpdateTotalCoins(int gotCoins)
    {
        this.statistics.totalCoins += gotCoins;
    }

    public void UpdateCoinsRaiting()
    {
        this.statistics.coinsRating = this.statistics.totalCoins / this.tryCounter;
    }

    public void UpdateTotalDistance(int newDist)
    {
        this.statistics.totalDistance += newDist;
    }

    public void UpdateDistanceRaiting()
    {
        this.statistics.distanceRating = this.statistics.totalDistance / this.tryCounter;
    }
}
