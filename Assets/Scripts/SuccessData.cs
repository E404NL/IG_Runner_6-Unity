//Object of this class will hold the data
//this object will be converted to JSON
[System.Serializable]
public class SuccessData
{
    public long id;
    public string condition;
    public int points;
    public string imageName;
}

public class Success
{
    private SuccessData successData;

    public Success(SuccessData data)
    {
        successData = data;
    }

    public long GetId()
    {
        return successData.id;
    }

    public void SetId(long newId)
    {
        successData.id = newId;
    }

    public string GetCondition()
    {
        return successData.condition;
    }

    public void SetCondition(string newCondition)
    {
        successData.condition = newCondition;
    }

    public int GetPoints()
    {
        return successData.points;
    }

    public void SetPoints(int newPoints)
    {
        successData.points = newPoints;
    }

    public string GetImageName()
    {
        return successData.imageName;
    }

    public void SetImageName(string newImageName)
    {
        successData.imageName = newImageName;
    }
}