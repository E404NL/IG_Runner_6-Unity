using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float turnSpeed = 90f;         //vitesse de rotation

    private void OnTriggerEnter (Collider other)    //gestion du trigger des pi�ces
    {
        if(other.gameObject.GetComponent<Obstacle>() != null)   //si la pi�ce spawn dans un obstacle : on d�truit la pi�ce
        {
            Destroy(gameObject);
            return;
        }

        if (other.gameObject.name != "Player")
        {
            return;
        }
        else if (other.gameObject.name == "Player")
        {
            Destroy(gameObject);
        }

        GameManager.inst.IncrementScore();
        UserAccess.instance.user.statistics.totalCoins++;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
    }
}
