using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;  //transform player
    Vector3 offset;                     //transform de la cam�ra


    // Start is called before the first frame update
    private void Start()
    {
        offset = transform.position - player.position;      //position de la cam�ra toujours derri�rele joueur
    }

    // Update is called once per frame
    private void Update()
    {
        //transform.position = player.position + offset;
        Vector3 targetPos = player.position + offset;
        targetPos.x = 0;
        transform.position = targetPos;
        
    }
}
