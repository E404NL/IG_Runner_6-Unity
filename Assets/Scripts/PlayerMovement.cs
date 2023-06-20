using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    bool alive = true;                          //En vie 
    public float speed= 5;                      //player speed
    [SerializeField] Rigidbody rb;              //Rigidbody (player)
    public float speedIncreasePerPoint;         //Valeur de l'accélération
    int distance = 0;

    [SerializeField] float jumpForce = 400f;    //force de saut
    [SerializeField] LayerMask groundMask;      //

    float height = 0f;                          //hauteur
    private bool isGrounded;                    //si ça touche le sol
    private bool isJumping;                     //si il est en l'air

    float horizontalInput;                      //touche de déplacement
    public float horizontalMultiplier = 2;      //multiplicateur de déplacement sur l'horizontale (vitesse du joueur * 2)

    Animator anim;                              //animator pour la gestion d'animations du personnage
    int Grounded = Animator.StringToHash("isGrounded");     //animation courir
    int jump = Animator.StringToHash("Jump");               //animation sauter
    int death = Animator.StringToHash("Death");             //animation mourir

    const bool _true = true;                                //booléan pour dire vrai
    
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();

    }

    private void Start()
    {
        height = GetComponent<Collider>().bounds.size.y;
    }

    private void FixedUpdate ()
    {
        if (!alive) return;
        
        //vérifier si on est au sol
        isGrounded = Physics.Raycast(transform.position, Vector3.down, (height / 2) + 0.1f, groundMask);

        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
        rb.MovePosition(rb.position + forwardMove + horizontalMove);
        distance = (int) transform.position.z;
        if(distance > GameManager.inst.distance)
        {
            GameManager.inst.IncrementDistance();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if(transform.position.x > 5) //Empêcher le Player d'aller au delà de X = 5
        {
            Vector3 rightPos = new Vector3(5, transform.position.y, transform.position.z);
            transform.position = rightPos;
            
        }else if (transform.position.x < -5) //Empêcher le Player d'aller au delà de X = -5
        {
            Vector3 leftPos = new Vector3(-5, transform.position.y, transform.position.z);
            transform.position = leftPos;
        }

        horizontalInput = Input.GetAxis("Horizontal"); //déplacement sur X

        if (transform.position.y < -5) //Si le player tombe dans le vide (au cas où)
        {
            Die();
        }
    }

    public void Die()
    {
        UserAccess.instance.user.tryCounter ++;
        if (UserAccess.instance.user.tryCounter >= 3)
        {
            GameOverManager.instance.RetryButton.interactable = false;
        }
        GameOverManager.instance.OnPlayerDeath();
        isGrounded = false;
        alive = false;
        anim.SetTrigger("Death");     //animation mort
        UserAccess.instance.user.UpdateCoinsRaiting();
        UserAccess.instance.user.UpdateTotalDistance(GameManager.inst.distance);
        UserAccess.instance.user.UpdateDistanceRaiting();
        UserAccess.instance.user.UpdateTotalScore();
        UserAccess.instance.user.PutUser();
        //Invoke("Restart", 2);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Jump()
    {
        if(isGrounded == true)  //si on est au sol, on peut sauter
        {
            anim.SetTrigger("Jump");
            rb.AddForce(Vector3.up * jumpForce);
        }
        else if(isGrounded == false){
            isJumping = false;
        }
    }
}
