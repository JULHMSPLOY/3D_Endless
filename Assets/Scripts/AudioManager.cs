using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------- Audio Source ----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------- Audio Clip ----------")]
    public AudioClip music;
    public AudioClip jump;
    public AudioClip coin;
    public AudioClip death;
    public AudioClip checkpoint;
    public AudioClip wallTouch;

    private void Start()
    {
        musicSource.clip = music;
        musicSource.Play();
    }
    public void PlaySFX(AudioClip clip) 
    { 
        SFXSource.PlayOneShot(clip);
    }
}
