using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Rewired;
using Rewired.ControllerExtensions;

public class playerController : MonoBehaviour
{
    public int PLAYERNUMBER;
    private Player p;
    private Rigidbody rb;
    private GameObject cam;

    public float movementSpeed, rotationSpeed, jumpForce;
    public float pitch, yaw;
    private Vector3 worldVelocity;

    public GameObject bullet;
    public Transform spawnPoint;
    public GameObject[] screens;
    public float screenTime;
    public float bulletSpeed;

    public float zMin, zMax;

    public int bulletsSpent;

    public GameObject gun;
    public GameObject currentGun;

    private int currentScene;
    public savePlayers saver;

    public loadScene load;

    public GameObject head, arm, pelvis;
    private Vector3 armBaseRot;

    public Animator anim;

    public bool isGrounded;
    
    public Quaternion orientation;
    private Controller c;

    void Start ()
    {
        p = ReInput.players.GetPlayer(PLAYERNUMBER);
        rb = GetComponent<Rigidbody>();
        cam = transform.GetChild(0).gameObject;
        currentGun = cam.transform.GetChild(0).gameObject;

        currentScene = SceneManager.GetActiveScene().buildIndex;

        c = p.controllers.GetController(ControllerType.Joystick, PLAYERNUMBER);

        isGrounded = true;

        if (currentScene == 1)
        {
            if (PLAYERNUMBER == 0)
            {
                transform.position = savePlayers.p1Pos;
                pitch = savePlayers.p1Rot.x;
                yaw = savePlayers.p1Rot.y;
                bulletsSpent = savePlayers.p1Bullet;
            }
            if (PLAYERNUMBER == 1)
            {
                transform.position = savePlayers.p2Pos;
                pitch = savePlayers.p2Rot.x;
                yaw = savePlayers.p2Rot.y;
                bulletsSpent = savePlayers.p2Bullet;
            }
        }
    }

    void Update ()
    {
        orientation = c.GetExtension<DualShock4Extension>().GetOrientation();

        worldVelocity = new Vector3(movementSpeed * p.GetAxis("Move S"), rb.velocity.y, movementSpeed * p.GetAxis("Move F"));
        rb.velocity = transform.TransformDirection(worldVelocity);
        if (p.GetAxis("Move F") >= 0)
            pelvis.transform.localEulerAngles = new Vector3(0, p.GetAxis("Move S") * 50, 0);
        else
            pelvis.transform.localEulerAngles = new Vector3(0, p.GetAxis("Move S") * -50, 0);

        //yaw and pitch via right stick
        yaw += rotationSpeed * p.GetAxis("Look X");
        pitch -= rotationSpeed * p.GetAxis("Look Y");

        //yaw and pitch via gyro
        //if (Mathf.Abs(orientation.z) > 0.2)
        //{
        //    yaw -= rotationSpeed * orientation.z * 2;
        //}
        //if (Mathf.Abs(orientation.x) > 0.2)
        //{
        //    pitch += rotationSpeed * orientation.x * 2;
        //}

        if (pitch > 75)
            pitch = 75;
        if (pitch < -75)
            pitch = -75;

        Vector3 lookUpAndDown = new Vector3(pitch, transform.eulerAngles.y, 0);
        cam.transform.eulerAngles = lookUpAndDown;
        head.transform.eulerAngles = lookUpAndDown;
        arm.transform.eulerAngles = lookUpAndDown;
        transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);

        if (p.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
            isGrounded = false;
        }

        if (p.GetButtonDown("Shoot"))
        {
            Shoot();
        }

        if (currentScene == 1)
        {
            if (p.GetButtonDown("Pause"))
            {
                saver.Save();
                load.StartCoroutine(load.Load(2));
            }
        }

        Debug.DrawRay(spawnPoint.transform.position, spawnPoint.transform.forward * 100, Color.red);

        anim.SetFloat("y velocity", rb.velocity.y);
        float speed = new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude;
        anim.SetFloat("speed", speed);
        anim.SetBool("is grounded", isGrounded);

        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(0);
    }

    void Shoot()
    {
        if (bulletsSpent < 6)
        {
            GameObject b = Instantiate(bullet, spawnPoint.position, Quaternion.identity);
            b.GetComponent<Rigidbody>().AddForce(spawnPoint.transform.forward * bulletSpeed);
            b.GetComponent<bullet>().playerNumber = PLAYERNUMBER + 1;
            bulletsSpent++;

            if (!activateDisplays.splitScreen)
            {
                GameObject[] popups = new GameObject[2];
                popups[0] = Instantiate(screens[0], screens[0].transform.parent);
                popups[1] = Instantiate(screens[1], screens[1].transform.parent);
                float r = Random.Range(zMin, zMax);
                foreach (GameObject p in popups)
                {
                    p.SetActive(true);
                    p.transform.GetChild(0).GetComponent<Text>().text = "BANG!";
                    p.transform.GetChild(0).GetComponent<Text>().transform.eulerAngles = new Vector3(0, 0, r);
                }
            }
            else
            {
                GameObject popup = Instantiate(screens[2], screens[2].transform.parent);
                float r = Random.Range(zMin, zMax);
                popup.SetActive(true);
                popup.transform.GetChild(0).GetComponent<Text>().text = "BANG!";
                popup.transform.GetChild(0).GetComponent<Text>().transform.eulerAngles = new Vector3(0, 0, r);
            }
        }
        else
        {
            currentGun.GetComponent<Animator>().SetBool("spent", true);
            currentGun.layer = 8;
            for (int i = 0; i < currentGun.transform.childCount; i++)
                currentGun.transform.GetChild(i).gameObject.layer = 8;
        }
    }

    public void NewGun()
    {
        GameObject g = Instantiate(gun, cam.transform);
        spawnPoint = g.transform.GetChild(2);
        currentGun = g;
        bulletsSpent = 0;
        currentGun.layer = PLAYERNUMBER + 11;
        for (int i = 0; i < currentGun.transform.childCount; i++)
            currentGun.transform.GetChild(i).gameObject.layer = PLAYERNUMBER + 11;
    }
}
