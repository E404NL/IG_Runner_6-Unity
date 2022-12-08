using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroundTile : MonoBehaviour
{
    GroundSpawner groundSpawner;
    List<GameObject> coinList = new List<GameObject>();
    [SerializeField] GameObject obstaclePrefab;         //objet d'obstacle en test (facultatif)
    [SerializeField] GameObject coinPrefab;             //objet de "pi�ce"
    [SerializeField] List<GameObject> lanes;            //liste de couroutines qui servent de spawn de pi�ces ce qui d�finira une ligne de spawn � chaque Tiles
    [SerializeField] List<GameObject> obstacleRandom;   //liste des objets d'obstacles qui seront choisis al�atoirement
    [SerializeField] List<GameObject> decorRandom;      //liste des objets de d�cor qui seront choisis al�atoirement
    [SerializeField] GameObject decorPrefab;            //objet de d�cor en test (facultatif)
    [SerializeField] List<GameObject> decorLanesRight;  //lignes de spawn des d�cors droits
    [SerializeField] List<GameObject> decorLanesLeft;   //lignes de spawn des d�cors gauches
    [SerializeField] List<GameObject> aorusPanels;      //liste des panneaux Aorus

    // Start is called before the first frame update
    private void Start()
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
    }

    private void OnTriggerExit(Collider other) //Si un object spawn sur un obstacle
    {
        groundSpawner.SpawnTile(true);
        Destroy(gameObject, 2);     //Destruction de l'objet
    }

    public void SpawnObstacle ()
    {
        //choisir une position random pour le spawn de l'obtacle
        int obstacleSpawnIndex = Random.Range(2, 5);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;
        //Spawn de l'obsacle sur la position
        GameObject temp = obstacleRandom[Random.Range(0, 3)]; //on d�fini un obstacle random entre trois objets
        Instantiate(temp, spawnPoint.position, Quaternion.identity, transform);   
    }

    public void SpawnDecorRight() //Spawn des �lements de d�cord droit
    {
        Vector3 spawnDecor = decorLanesRight[Random.Range(0,3)].transform.position;     //position d�finie pour le pawn de l'�l�ment de d�cor
        
        //Spawn du decor sur la position
        GameObject temp = decorRandom[Random.Range(0, 5)];                  //d�fini le d�cor random entre 3 objets de d�cor
        Instantiate(temp, spawnDecor, Quaternion.identity, transform);
    }

    public void SpawnDecorLeft() //Spawn des �lements de d�cord gauche
    {
        Vector3 spawnDecor = decorLanesLeft[Random.Range(0, 3)].transform.position;     //position d�finie pour le pawn de l'�l�ment de d�cor
        //Spawn du decor sur la position
        GameObject temp = decorRandom[Random.Range(0, 5)];                  //d�fini le d�cor random entre 3 objets de d�cor
        Instantiate(temp, spawnDecor, Quaternion.identity, transform);
    }

    public void SpawnCoins () //spawn des coins sur la surface
    {
        int coinsToSpawn = 5; //nombre de coin par GroundTile
        float x = lanes[Random.Range(0, 3)].transform.position.x; //on d�fini sa coordonn�e X de mani�re random entre -2.5, 0, 2.5
        for (int i = 0; i< coinsToSpawn; i++)
        {
            GameObject temp = Instantiate(coinPrefab, transform);   //on instancie l'objet coin
            coinList.Add(temp);
            temp.transform.position = GetRandomPointInCollider(GetComponent<Collider>(), x); //appelle de la fonction de coordonn�es random
        }
    }

    Vector3 GetRandomPointInCollider (Collider collider, float x) //coordonn�es al�atoires des coins sur la surface avec la coordon�e x
    {
        Vector3 point = new Vector3(
            x, //coordonn�e X d�j� d�finie dans SpawnCoin()
            Random.Range(collider.bounds.min.y, collider.bounds.max.y), //au hasard sur une hauteur qui bouge pas (tout va bien)
            Random.Range(collider.bounds.min.z, collider.bounds.max.z) //au hasard un spawn sur Z
            );
        if (point != collider.ClosestPoint(point))
        {
            point = GetRandomPointInCollider(collider, x);
        }
        point.y = 1;
        return point;
    }

    private void OnDestroy() //destruction des objets coins derri�re le joueur
    {
        for(int i = 0; i<coinList.Count; i++)
        {
            Destroy(coinList[i]);
        }
    }
}
