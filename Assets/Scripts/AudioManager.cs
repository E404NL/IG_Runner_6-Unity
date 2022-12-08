using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip[] playlist;  //Liste audio
    public AudioSource audioSource;     //objet de lecture de musique
    private int musicIndex = 0;     //numéro de la musique jouée


    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = playlist[0];
        audioSource.Play(); //Lecture de la musque
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying) //Si la musique ne tourne pas, on la relance
        {
            if(playlist.Length == 1)
                audioSource.Play(); //Si on utilise une musique
            else if(playlist.Length > 1)
                PlayNextSong();  //Si on utilise plus d'une musique
        }
    }

    void PlayNextSong() //jouer plusieurs musiques
    {
        musicIndex = (musicIndex + 1) % playlist.Length;    //on récupère la valeur de placement de la musique suivante (dans la liste)
        audioSource.clip = playlist[musicIndex];            //on joue la musique déduite juste avant
        audioSource.Play();                                 //
    }
}
