using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject one, two;
    private Transform o, t;
    private bool hasOne, hasTwo;
    public GameObject play, quit, shoot;
    public GameObject countdown, inst;

    public loadScene load;

    private void Start()
    {
        o = transform.GetChild(0);
        t = transform.GetChild(1);
    }

    private void Update()
    {
        if (one.transform.position == o.position)
            hasOne = true;
        else
            hasOne = false;
        if (two.transform.position == t.position)
            hasTwo = true;
        else
            hasTwo = false;

        if (hasOne && hasTwo)
            GetComponent<Button>().onClick.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("shot by player " + collision.gameObject.GetComponent<bullet>().playerNumber);
            if (collision.gameObject.GetComponent<bullet>().playerNumber == 1)
            {
                one.SetActive(true);
                one.transform.position = o.position;
            }
            if (collision.gameObject.GetComponent<bullet>().playerNumber == 2)
            {
                two.SetActive(true);
                two.transform.position = t.position;
            }
        }
    }

    public void StartGame()
    {
        StartCoroutine(ActuallyStartGame());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RemoveInst()
    {
        inst.SetActive(false);
        shoot.SetActive(false);
        one.SetActive(false);
        two.SetActive(false);
        play.SetActive(true);
        quit.SetActive(true);
    }

    private IEnumerator ActuallyStartGame()
    {
        play.GetComponent<MeshRenderer>().enabled = false;
        quit.SetActive(false);
        one.SetActive(false);
        two.SetActive(false);
        countdown.SetActive(true);
        yield return new WaitForSeconds(10.0f);
        Debug.Log("finished waiting");
        load.StartCoroutine(load.Load(1));
        Debug.Log("loading");
    }
}
