//Object of this class will hold the data
//this object will be converted to JSON
[System.Serializable]
public class StatisticsData
{
    private long _id;
    private int _totalCoins;
    private int _actualCoins;
    private float _coinsRating;
    private int _totalDistance;
    private float _distanceRating;

    public StatisticsData()
    {
        this._totalCoins = 0;
        this._actualCoins = 0;
        this._coinsRating = 0;
        this._totalDistance = 0;
        this._distanceRating = 0;
    }

    public StatisticsData(StatisticsData stats)
    {
        this.id = stats.id;
        this.totalCoins = stats.totalCoins;
        this.actualCoins = stats.actualCoins;
        this.coinsRating = stats.coinsRating;
        this.totalDistance = stats.totalDistance;
        this.distanceRating = stats.distanceRating;
    }

    public long id
    {
        get => _id;
        set => _id = value;
    }

    public int totalCoins
    {
        get => _totalCoins;
        set => _totalCoins = value;
    }

    public int actualCoins
    {
        get => _actualCoins;
        set => _actualCoins = value;
    }

    public float coinsRating
    {
        get => _coinsRating;
        set => _coinsRating = value;
    }

    public int totalDistance
    {
        get => _totalDistance;
        set => _totalDistance = value;
    }

    public float distanceRating
    {
        get => _distanceRating;
        set => _distanceRating = value;
    }

    public void UpdateTotalCoins(int gotCoins)
    {
        this._totalCoins += gotCoins;
    }

    public void UpdateCoinsRaiting(int counter)
    {
        this._coinsRating = this._totalCoins / counter;
    }

    public void UpdateTotalDistance(int newDist)
    {
        this._totalDistance += newDist;
    }

    public void UpdateDistanceRaiting(int counter)
    {
        this._distanceRating = this._totalDistance / counter;
    }
}