using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header("-----------Audio Source----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("-----------Audio Clip----------")]
    public AudioClip Background;


    private void Start()
    {
        musicSource.clip = Background;
         musicSource.loop = true; 
        musicSource.Play();
    }

}
