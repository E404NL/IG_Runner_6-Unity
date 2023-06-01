//Object of this class will hold the data
//this object will be converted to JSON

using UnityEngine;
using UnityEngine.Networking;


[System.Serializable]
public class UserData
{
    private long id;
    private string username;
    private string password;
    private string email;
    private string name;
    private string firstname;
    private int age;
    private string gender;
    private int codePostal;
    private long rank;
    private int tryCounter;
    private string actualSkin;
    private int totalScore;
    private StatisticsData statistics;
    private GrindData grind;
    /*public SuccessData[] successes;*/

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
        this.actualSkin = "default";
        this.totalScore = 0;
        this.statistics = new StatisticsData();
        this.grind = new GrindData();
    }

    public UserData(UserData user)
    {
        this.Id = user.Id;
        this.Username = user.Username;
        this.Password = user.Password;
        this.Email = user.Email;
        this.Name = user.Name;
        this.Firstname = user.Firstname;
        this.Age = user.Age;
        this.Gender = user.Gender;
        this.CodePostal = user.CodePostal;
        this.Rank = user.Rank;
        this.ActualSkin = user.ActualSkin;
        this.TotalScore = user.TotalScore;
        this.Statistics = user.Statistics;
        this.Grind = user.Grind;
    }

    public long Id
    {
        get => id;
        set => id = value;
    }

    public string Username
    {
        get => username;
        set => username = value;
    }

    public string Password
    {
        get => password;
        set => password = value;
    }

    public string Email
    {
        get => email;
        set => email = value;
    }

    public string Name
    {
        get => name;
        set => name = value;
    }

    public string Firstname
    {
        get => firstname;
        set => firstname = value;
    }

    public int Age
    {
        get => age;
        set => age = value;
    }

    public string Gender
    {
        get => gender;
        set => gender = value;
    }

    public int CodePostal
    {
        get => codePostal;
        set => codePostal = value;
    }

    public long Rank
    {
        get => rank;
        set => rank = value;
    }

    public int TryCounter
    {
        get => tryCounter;
        set => tryCounter = value;
    }

    public string ActualSkin
    {
        get => actualSkin;
        set => actualSkin = value;
    }

    public int TotalScore
    {
        get => totalScore;
        set => totalScore = value;
    }

    public StatisticsData Statistics
    {
        get => statistics;
        set => statistics = value;
    }

    public GrindData Grind
    {
        get => grind;
        set => grind = value;
    }

    public void UpdateUser()
    {
        string url = "http://localhost:8080/ws/users/" + this.id; // URL de l'endpoint pour récupérer un utilisateur par son ID

        UnityWebRequest request = UnityWebRequest.Get(url);

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
            Debug.LogError("Error when try to get User : " + request.error);
        }
    }

    public void UpdateTotalScore()
    {
        StatisticsData stats = this.statistics;
        GrindData grind = this.grind;
        int success_points = grind.SuccessPoints;
        float dist = stats.TotalDistance;
        int coins = stats.TotalCoins;
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
        this.statistics.TotalCoins += gotCoins;
    }

    public void UpdateCoinsRaiting()
    {
        this.statistics.CoinsRating = this.statistics.TotalCoins / this.tryCounter;
    }

    public void UpdateTotalDistance(int newDist)
    {
        this.statistics.TotalDistance += newDist;
    }

    public void UpdateDistanceRaiting()
    {
        this.statistics.DistanceRating = this.statistics.TotalDistance / this.tryCounter;
    }
}
