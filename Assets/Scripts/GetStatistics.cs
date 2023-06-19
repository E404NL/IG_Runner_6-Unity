using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GetStatistics : MonoBehaviour
{
    public Text Pseudo;
    public Text TotalDist;
    public Text RatingDist;
    public Text TotalCoins;
    public Text RaitingCoins;
    public Text NTry;
    public Text TotalScore;
    public Text Rank;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetStats()
    {
        Debug.Log("Setting User Statistics !");
        Pseudo.text = UserAccess.instance.user.username;
        TotalDist.text = UserAccess.instance.user.statistics.totalDistance.ToString();
        RatingDist.text = UserAccess.instance.user.statistics.distanceRating.ToString();
        TotalCoins.text = UserAccess.instance.user.statistics.totalCoins.ToString();
        RaitingCoins.text = UserAccess.instance.user.statistics.coinsRating.ToString();
        TotalScore.text = UserAccess.instance.user.totalScore.ToString();
        NTry.text = ( 3 - UserAccess.instance.user.tryCounter ).ToString();
        Rank.text = UserAccess.instance.user.rank.ToString();
    }
}
