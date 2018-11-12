using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reelAudio : MonoBehaviour
{
    private AudioSource source;
    public AudioClip start, looped;
    public float waitTime;
    
	void Awake ()
    {
        source = GetComponent<AudioSource>();
        source.clip = start;
        source.Play();
        StartCoroutine(startReel());
	}

    private IEnumerator startReel()
    {
        yield return new WaitForSeconds(waitTime);
        if (GetComponent<gameTimer>() != null)
            GetComponent<gameTimer>().enabled = true;
        source.clip = looped;
        source.loop = true;
        source.Play();
    }
}
