using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tumbleweed : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 direction;
    public float speed, increment, incTime, upwardsForce;
    
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(setDirection());
	}

    IEnumerator setDirection()
    {
        while (true)
        {
            float incX = Random.Range(-increment, increment);
            float incZ = Random.Range(-increment, increment);
            Vector3 force = new Vector3(direction.x + incX, upwardsForce, direction.z + incZ);
            rb.velocity = Vector3.zero;
            rb.AddForce(force * speed);
            direction = force;
            yield return new WaitForSeconds(incTime);
        }
    }
}
