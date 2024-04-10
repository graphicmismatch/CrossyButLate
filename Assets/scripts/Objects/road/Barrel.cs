using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{

    public GameObject explodeyThing;


    public void explode()
    {
        explodeyThing.SetActive(true);
    }
}
