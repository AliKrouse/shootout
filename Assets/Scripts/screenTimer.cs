using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenTimer : MonoBehaviour
{
    public float timer;
    private float timeRemaining;

    void OnEnable()
    {
        timeRemaining = timer;
    }

    void Update ()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
            Destroy(this.gameObject);
	}
}
