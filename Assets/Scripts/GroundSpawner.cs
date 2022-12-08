using UnityEngine;

public class GroundSpawner : MonoBehaviour
{

    [SerializeField] GameObject groundTile;     //objet GroundTile
    Vector3 nextSpawnPoint;                     //transform 3D

    public void SpawnTile(bool spawnItems)      //application des m�thodes associ�es au groundTile
    {
        GameObject temp = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity);     //Instancie le groundTile
        nextSpawnPoint = temp.transform.GetChild(1).transform.position;                     //on r�cup�re son transform
        temp.transform.SetParent(transform);                                                //on met un parent au transform du groundTile

        if (spawnItems)     //application des m�thodes li�es au groundTile
        {
            temp.GetComponent<GroundTile>().SpawnObstacle();    //obstacles
            temp.GetComponent<GroundTile>().SpawnCoins();       //pi�ces
            temp.GetComponent<GroundTile>().SpawnDecorRight();  //d�cors � droite
            temp.GetComponent<GroundTile>().SpawnDecorLeft();   //d�cors � gauche
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i<15; i++)      //spawn de groundTile
        {
            if (i < 2)      //tant qu'on est pas aux deuxi�me tile
            {
                SpawnTile(false);   //Pas de spawn d'obstacles et des d�cors
            }
            else
            {
                SpawnTile(true);    //spawn des obstacles et des d�cors
            }
        }
    }
}
