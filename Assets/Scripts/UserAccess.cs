using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserAccess : MonoBehaviour
{
    public static UserAccess instance;
    public UserData user;

    // Start is called before the first frame update
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void disconnect()
    {
        Destroy(instance);
    }
}
