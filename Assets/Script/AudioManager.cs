using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
   public static AudioManager Instance;
   public Sound[] musicSounds, sfxSounds;
   public AudioSource musicSource, sfxSource;

   public AudioClip Theme;
   public AudioClip Death;
   public AudioClip Jump;
   public AudioClip Win;



    private void Awake() {

        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() 
    {
        musicSource.clip = Theme;// musica que suene al inicio
        musicSource.Play();
    }

    public void PlayMusic(string name)
    {
          Sound s = Array.Find(musicSounds, (s) => s.name == name);

          if (s != null)
          {
            Debug.Log("Sound not found");
          }
          else 
          {
            musicSource.clip = s.clip;
            musicSource.Play();
          }
    }

    
    public void PlaySFX(AudioClip clip)
    {
         sfxSource.PlayOneShot(clip);
    }
 
 

}
