using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameTimer : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip music, end;
    public float timer;
    public float waitAfterCut;
    public bool gameEnded;
    public GameObject[] screens;

    public loadScene load;
    
	void OnEnable ()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = music;

        timer = savePlayers.timeRemaining;
        musicSource.time = savePlayers.musicPoint;

        musicSource.Play();
	}
	
	void Update ()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && !gameEnded)
            StartCoroutine(endGame());
	}

    public IEnumerator endGame()
    {
        gameEnded = true;

        musicSource.Stop();
        musicSource.clip = end;
        musicSource.Play();

        foreach (GameObject g in screens)
        {
            g.SetActive(true);
            g.transform.GetChild(0).GetComponent<Text>().text = "fin.";
            g.GetComponent<screenTimer>().enabled = false;
        }

        yield return new WaitForSeconds(waitAfterCut);
        
        load.StartCoroutine(load.Load(0));
    }
}
