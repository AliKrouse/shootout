using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundPlayer : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<playerController>().isGrounded = true;
        }
    }
}
