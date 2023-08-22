using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Vector3 offset;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (!CameraShake.shaking)
        {
            this.transform.position = new Vector3(offset.x, offset.y, offset.z + player.transform.position.z);
        }

        this.transform.eulerAngles = new Vector3(transform.eulerAngles.x, player.transform.position.x*1.1f, transform.eulerAngles.z);
    }
}
