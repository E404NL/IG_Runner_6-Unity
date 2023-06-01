using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score;                               //score
    public int distance;                            //distance
    public static GameManager inst;                 //gameManager
    [SerializeField] Text scoreText;                //String du score
    [SerializeField] Text distanceText;             //String de la distance
    [SerializeField] PlayerMovement playerMovement; //objet plaerMovement


    public void IncrementScore() //méthode d'incrémentation du score
    {
        score++;                            //Incrémentation du score
        scoreText.text = "SCORE : " + score; //affichage de la valeur du score

        //vitesse du joueur
        playerMovement.speed += playerMovement.speedIncreasePerPoint;   //incrémentation de la vitesse du player
    }

    public void IncrementDistance()
    {
        distance++;
        distanceText.text = "DISTANCE : " + distance;
    }
    private void Awake()
    {
        inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
