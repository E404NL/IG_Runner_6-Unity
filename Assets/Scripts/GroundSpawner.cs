using UnityEngine;

public class GroundSpawner : MonoBehaviour
{

    [SerializeField] GameObject groundTile;     //objet GroundTile
    Vector3 nextSpawnPoint;                     //transform 3D

    public void SpawnTile(bool spawnItems)      //application des méthodes associées au groundTile
    {
        GameObject temp = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity);     //Instancie le groundTile
        nextSpawnPoint = temp.transform.GetChild(1).transform.position;                     //on récupère son transform
        temp.transform.SetParent(transform);                                                //on met un parent au transform du groundTile

        if (spawnItems)     //application des méthodes liées au groundTile
        {
            temp.GetComponent<GroundTile>().SpawnObstacle();    //obstacles
            temp.GetComponent<GroundTile>().SpawnCoins();       //pièces
            temp.GetComponent<GroundTile>().SpawnDecorRight();  //décors à droite
            temp.GetComponent<GroundTile>().SpawnDecorLeft();   //décors à gauche
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i<15; i++)      //spawn de groundTile
        {
            if (i < 2)      //tant qu'on est pas aux deuxième tile
            {
                SpawnTile(false);   //Pas de spawn d'obstacles et des décors
            }
            else
            {
                SpawnTile(true);    //spawn des obstacles et des décors
            }
        }
    }
}
