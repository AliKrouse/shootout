using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public int playerNumber;

    private void Awake()
    {
        StartCoroutine(timer());
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }

    private IEnumerator timer()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
}
