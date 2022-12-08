using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    bool alive = true;                          //En vie 
    public float speed= 5;                      //player speed
    [SerializeField] Rigidbody rb;              //Rigidbody (player)
    public float speedIncreasePerPoint;         //Valeur de l'acc�l�ration

    [SerializeField] float jumpForce = 400f;    //force de saut
    [SerializeField] LayerMask groundMask;      //

    float height = 0f;                          //hauteur
    private bool isGrounded;                    //si �a touche le sol
    private bool isJumping;                     //si il est en l'air

    float horizontalInput;                      //touche de d�placement
    public float horizontalMultiplier = 2;      //multiplicateur de d�placement sur l'horizontale (vitesse du joueur * 2)

    Animator anim;                              //animator pour la gestion d'animations du personnage
    int Grounded = Animator.StringToHash("isGrounded");     //animation courir
    int jump = Animator.StringToHash("Jump");               //animation sauter
    int death = Animator.StringToHash("Death");             //animation mourir

    const bool _true = true;                                //bool�an pour dire vrai
    
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
        
        //v�rifier si on est au sol
        isGrounded = Physics.Raycast(transform.position, Vector3.down, (height / 2) + 0.1f, groundMask);

        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
        rb.MovePosition(rb.position + forwardMove + horizontalMove);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if(transform.position.x > 5) //Emp�cher le Player d'aller au del� de X = 5
        {
            Vector3 rightPos = new Vector3(5, transform.position.y, transform.position.z);
            transform.position = rightPos;
            
        }else if (transform.position.x < -5) //Emp�cher le Player d'aller au del� de X = -5
        {
            Vector3 leftPos = new Vector3(-5, transform.position.y, transform.position.z);
            transform.position = leftPos;
        }

        horizontalInput = Input.GetAxis("Horizontal"); //d�placement sur X

        if (transform.position.y < -5) //Si le player tombe dans le vide (au cas o�)
        {
            Die(); //B�Il�Mort CHEH
        }
    }

    public void Die() //bah il est plus alive quoi
    {
        GameOverManager.instance.OnPlayerDeath();
        isGrounded = false;
        alive = false;
        anim.SetTrigger("Death");   //animation wallah t� mor
        //Invoke("Restart", 2);       //Invoque la m�thode Restart
    }

    void Restart()  //recommencer la partie
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //rechargement de la sc�ne
    }

    void Jump() //sauter
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