//Object of this class will hold the data
//this object will be converted to JSON
[System.Serializable]
public class StatisticsData
{
    private long id;
    private int totalCoins;
    private int actualCoins;
    private float coinsRating;
    private int totalDistance;
    private float distanceRating;

    public StatisticsData()
    {
        this.totalCoins = 0;
        this.actualCoins = 0;
        this.coinsRating = 0;
        this.totalDistance = 0;
        this.distanceRating = 0;
    }

    public StatisticsData(StatisticsData stats)
    {
        this.Id = stats.Id;
        this.TotalCoins = stats.TotalCoins;
        this.ActualCoins = stats.ActualCoins;
        this.CoinsRating = stats.CoinsRating;
        this.TotalDistance = stats.TotalDistance;
        this.DistanceRating = stats.DistanceRating;
    }

    public long Id
    {
        get => id;
        set => id = value;
    }

    public int TotalCoins
    {
        get => totalCoins;
        set => totalCoins = value;
    }

    public int ActualCoins
    {
        get => actualCoins;
        set => actualCoins = value;
    }

    public float CoinsRating
    {
        get => coinsRating;
        set => coinsRating = value;
    }

    public int TotalDistance
    {
        get => totalDistance;
        set => totalDistance = value;
    }

    public float DistanceRating
    {
        get => distanceRating;
        set => distanceRating = value;
    }

    public void UpdateTotalCoins(int gotCoins)
    {
        this.totalCoins += gotCoins;
    }

    public void UpdateCoinsRaiting(int counter)
    {
        this.coinsRating = this.totalCoins / counter;
    }

    public void UpdateTotalDistance(int newDist)
    {
        this.totalDistance += newDist;
    }

    public void UpdateDistanceRaiting(int counter)
    {
        this.distanceRating = this.totalDistance / counter;
    }
}