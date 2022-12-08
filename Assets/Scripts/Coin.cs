using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float turnSpeed = 90f;         //vitesse de rotation

    private void OnTriggerEnter (Collider other)    //gestion du trigger des pièces
    {
        if(other.gameObject.GetComponent<Obstacle>() != null)   //si la pièce spawn dans un obstacle : on détruit la pièce
        {
            Destroy(gameObject);
            return;
        }

        if (other.gameObject.name != "Player")      //Check que l'objet est collided avec le joueur
        {
            return;     //non : rien
        }
        else if (other.gameObject.name == "Player")
        {
            Destroy(gameObject);        //oui : détruire l'objet pièce
        }

        GameManager.inst.IncrementScore(); //appel de la méthode d'incrémentation de score du joueur
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
