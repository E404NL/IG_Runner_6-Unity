using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorTile : MonoBehaviour
{
    GroundSpawner groundSpawner;
    [SerializeField] List<GameObject> decorRandom;
    [SerializeField] GameObject decorPrefab;
    [SerializeField] List<GameObject> decorLanes;

    // Start is called before the first frame update
    void Start()
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
    }

    private void OnTriggerExit(Collider other) //Si un object spawn sur un obstacle
    {
        groundSpawner.SpawnTile(true);
        Destroy(gameObject, 2);     //Destruction de l'objet
    }

    public void SpawnDecor()
    {
        //Choisir une position random pour le pawn de l'élément de décor
        int decorSpawnIndex = Random.Range(0, 3);
        Transform spawnPoint = transform.GetChild(decorSpawnIndex).transform;
        //Spawn du decor sur la position
        GameObject temp = decorRandom[Random.Range(0, 3)]; //défini le décor random entre 3 objets de décor
        Instantiate(temp, spawnPoint.position, Quaternion.identity, transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
