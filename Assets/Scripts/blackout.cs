using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blackout : MonoBehaviour
{
    public bool fadeIn, fadeOut;
    public float timer;

    private Image i;
    private Color c;

    public float a;
    
	void Start ()
    {
        i = GetComponent<Image>();
        c = i.color;
        a = c.a;
	}
	
	void Update ()
    {
        if (fadeIn && a < 1)
        {
            //a = c.a;
            //a = Mathf.Lerp(a, 1, Time.deltaTime * timer);
            a += Time.deltaTime * timer;
            i.color = new Color(c.r, c.g, c.b, a);
        }

        if (fadeOut && a > 0)
        {
            //a = c.a;
            //a = Mathf.Lerp(a, 0, Time.deltaTime * timer);
            a -= Time.deltaTime * timer;
            i.color = new Color(c.r, c.g, c.b, a);
        }
	}
}
