using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSelector_Script : MonoBehaviour
{

    public bool isBossMusicPlaying = false;

    public AudioClip song1;
    public AudioClip song2;
    public AudioClip song3;
    public AudioClip song4;
    public AudioClip song5;
    

    public AudioSource audioSource_Music;

    private void Awake()
    {
        audioSource_Music = this.GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        PickSong();

    }

    
    // Update is called once per frame
    void Update()
    {
        if((audioSource_Music.isPlaying == false) && (isBossMusicPlaying == false))
        {
            PickSong();
        }
        
    }

    private void PickSong()
    {
        int i = Random.Range(1, 4);

        if (i == 1)
        {
            audioSource_Music.clip = song1;
            audioSource_Music.Play();
            Debug.Log("He reproducido " + song1.ToString());
            
        }
        else if (i == 2)
        {
            audioSource_Music.clip = song2;
            audioSource_Music.Play();
            Debug.Log("He reproducido " + song2.ToString());

        }
        else if (i == 3)
        {
            audioSource_Music.clip = song4;
            audioSource_Music.Play();
            Debug.Log("He reproducido " + song4.ToString());
        }
        else if (i == 4)
        {
            audioSource_Music.clip = song5;
            audioSource_Music.Play();
            Debug.Log("He reproducido " + song5.ToString());
        }
        
    }

    public void StopMusic()
    {
        audioSource_Music.Stop();
    }

    
    public IEnumerator PlayBossMusic(float secondsForBossMusic)
    {
        isBossMusicPlaying = true;
        audioSource_Music.Stop();
        audioSource_Music.clip = song3;
        audioSource_Music.loop = true;
       

        yield return new WaitForSeconds(secondsForBossMusic); //PARA QUE FUNCIONE WAITFORSECONDS HAY QUE PONER YIELD RETURN SIEMPRE!
        
        audioSource_Music.Play();
        
        //yield return null;
    }
}
