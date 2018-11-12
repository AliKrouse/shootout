using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateDisplays : MonoBehaviour
{
    public GameObject seperate, split;
    public static bool splitScreen;
    
	void Start ()
    {
        Debug.Log("displays connected: " + Display.displays.Length);
        // Display.displays[0] is the primary, default display and is always ON.
        // Check if additional displays are available and activate each.
        if (Display.displays.Length == 1)
        {
            split.SetActive(true);
            seperate.SetActive(false);
            splitScreen = true;
        }
        if (Display.displays.Length > 1)
        {
            Display.displays[1].Activate();
            split.SetActive(false);
            seperate.SetActive(true);
            splitScreen = false;
        }
    }
}
