using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject player;
    private Animator anim;
    public float throwForce;
    
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        player = transform.parent.transform.parent.gameObject;
        anim = GetComponent<Animator>();
	}

    public IEnumerator ThrowGun()
    {
        //Debug.Log("ready");
        anim.enabled = false;
        yield return new WaitForSeconds(0.01f);
        //Debug.Log("throwing");
        transform.SetParent(null, true);
        rb.isKinematic = false;
        rb.AddForce(player.transform.forward * throwForce);
        rb.AddTorque(transform.right * throwForce);
        SetLayerRecursively(gameObject, 0);
        yield return new WaitForSeconds(0.75f);
        //Debug.Log("drawing");
        player.GetComponent<playerController>().NewGun();
    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}
