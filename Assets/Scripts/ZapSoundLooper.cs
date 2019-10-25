using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZapSoundLooper : MonoBehaviour
{
    private int LoopDelay;
    private float pitcher;
    public AudioSource Source;


    void Start()
    {
        Source = gameObject.GetComponent<AudioSource>();
        StartCoroutine(Looper());
    }


    IEnumerator Looper()
    {
        while (true)
        {
            pitcher = Random.Range(.77f, 1);
            Source.pitch = pitcher;

            Source.Play();
            LoopDelay = Random.Range(0, 3);
            yield return new WaitForSeconds(LoopDelay);
        }
    }
}
