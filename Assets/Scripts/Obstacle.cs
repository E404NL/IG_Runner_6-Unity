using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    PlayerMovement playerMovement; //Appel de PlayerMovement.cs

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>(); //objet playerMovement
    }

    private void OnCollisionEnter (Collision collision)                 //Gestion collision avec les obstacles
    {
        if (collision.gameObject.name == "Player")                      //Si player touche l'objet
        {
            //kill player
            playerMovement.Die();                                       //Appel de la fonction Die de l'objet playerMovement;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
