using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{

    
    public Spawn[] dp;
    public GameObject[] spikes;

    public GameObject[] ramps;
    public GameObject[] barrels;
    public static bool spawn = true;
    // Start is called before the first frame update
    void Start()
    {
        if (spawn)
        {
            foreach (GameObject g in spikes)
            {
                g.SetActive(Random.Range(0, 23) == 5);
            }
            foreach (GameObject g in ramps)
            {
                g.SetActive(Random.Range(0, 50) == 5);
            }
            foreach (Spawn g in dp)
            {
                g.spawn(Random.Range(0, 4) == 1);
            }
            foreach (GameObject g in barrels)
            {
                g.SetActive(Random.Range(0, 100) == 1);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
