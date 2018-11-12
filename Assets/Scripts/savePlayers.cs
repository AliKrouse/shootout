using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class savePlayers : MonoBehaviour
{
    public static Vector3 p1Pos, p2Pos;
    public static Vector3 p1Rot, p2Rot;
    public static int p1Bullet, p2Bullet;

    public static float timeRemaining, musicPoint;

    public playerController p1, p2;
    public gameTimer timer;

    public void Save()
    {
        p1Pos = p1.gameObject.transform.position;
        p2Pos = p2.gameObject.transform.position;
        p1Rot = new Vector3(p1.pitch, p1.yaw, 0);
        p2Rot = new Vector3(p2.pitch, p2.yaw, 0);
        p1Bullet = p1.bulletsSpent;
        p2Bullet = p2.bulletsSpent;

        timeRemaining = timer.timer;
        musicPoint = timer.musicSource.time;
    }
}
