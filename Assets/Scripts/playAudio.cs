using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAudio : MonoBehaviour
{
    AudioSource audioData;

    public AudioClip[] screams;
    private AudioClip myScream;

    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();


        audioData.Play(0);
    }

    private void playSound()
    {
        int Array = Random.Range(0, screams.Length);
        myScream = screams[Array];
        audioData.clip = myScream;
        audioData.Play();
    }
}
