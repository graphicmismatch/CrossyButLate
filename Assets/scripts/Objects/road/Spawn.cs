using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] ducks;
    public void spawn(bool act)
    {
        if (act)
        {
            Instantiate(ducks[Random.Range(0, ducks.Length)], new Vector3(Random.Range(-11.75f, 11.75f), 1.77f, this.transform.position.z), Quaternion.identity);
        }
    }
}
