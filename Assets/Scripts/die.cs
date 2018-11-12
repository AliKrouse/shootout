using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class die : MonoBehaviour
{
    private playerController pc;
    private Rigidbody rb;
    private GameObject cam;
    private GameObject gun;
    private GameObject point;
    public float cameraZoomSpeed;

    public gameTimer gt;
    
	void Start ()
    {
        pc = GetComponent<playerController>();
        rb = GetComponent<Rigidbody>();
        cam = transform.GetChild(0).gameObject;
        gun = cam.transform.GetChild(0).gameObject;
        point = cam.transform.GetChild(2).gameObject;
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            pc.enabled = false;
            gun.SetActive(false);
            rb.velocity = Vector3.zero;
            cam.transform.SetParent(null, true);
            point.transform.SetParent(null, true);
        }
    }

    private void Update()
    {
        if (pc.enabled == false)
        {
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, point.transform.position, Time.deltaTime * cameraZoomSpeed);

            float d = Vector3.Distance(cam.transform.position, point.transform.position);
            if (d < float.Epsilon && !gt.gameEnded)
            {
                gt.StartCoroutine(gt.endGame());
            }
        }
    }
}
