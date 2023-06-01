//Object of this class will hold the data
//this object will be converted to JSON
[System.Serializable]
public class GrindData
{
    public long id;
    public int success_points;

    public GrindData()
    {
        this.success_points = 0;
    }

    public long Id
    {
        get => id;
        set => id = value;
    }

    public int SuccessPoints
    {
        get => success_points;
        set => success_points = value;
    }
}