using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setStartingPositions : MonoBehaviour
{
	void Start ()
    {
        savePlayers.p1Pos = new Vector3(0, 1, -8.5f);
        savePlayers.p2Pos = new Vector3(0, 1, 8.5f);
        savePlayers.p1Rot = new Vector3(0, 0, 0);
        savePlayers.p2Rot = new Vector3(0, 180, 0);
        savePlayers.p1Bullet = 0;
        savePlayers.p2Bullet = 0;

        savePlayers.timeRemaining = 187;
        savePlayers.musicPoint = 0;
	}
}
