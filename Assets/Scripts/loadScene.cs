using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadScene : MonoBehaviour
{
    public GameObject[] blackout;

    public IEnumerator Load(int sceneToLoad)
    {
        foreach (GameObject g in blackout)
        {
            g.SetActive(true);
            g.GetComponent<blackout>().fadeOut = false;
            g.GetComponent<blackout>().fadeIn = true;
        }

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(sceneToLoad);
    }
}
