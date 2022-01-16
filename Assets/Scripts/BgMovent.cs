using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMovent : MonoBehaviour
{

    public Transform bg1;
    public Transform bg2;
    public float speed = 1f;
    void Update()
    {
        if (GameStrateManager.GetState() != State.Playin && GameStrateManager.GetState() != State.None) return;

        bg1.transform.position -= new Vector3(speed * Time.deltaTime,0,0);
        bg2.transform.position -= new Vector3(speed * Time.deltaTime,0,0);
        if (bg1.transform.position.x < -7.3)
        {
            bg1.transform.position = bg2.transform.position + new Vector3(7.4f,0,0);
        }
        if (bg2.transform.position.x < -7.3)
        {
            bg2.transform.position = bg1.transform.position + new Vector3(7.4f, 0, 0);
        }
    }
}
