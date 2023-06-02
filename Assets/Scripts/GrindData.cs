//Object of this class will hold the data
//this object will be converted to JSON
[System.Serializable]
public class GrindData
{
    public long _id;
    public int _success_points;

    public GrindData()
    {
        this._success_points = 0;
    }

    public GrindData(GrindData grind)
    {
        this.id = grind.id;
        this.successPoints = grind.successPoints;
    }

    public long id
    {
        get => _id;
        set => _id = value;
    }

    public int successPoints
    {
        get => _success_points;
        set => _success_points = value;
    }
}