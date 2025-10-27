using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class roadgen : MonoBehaviour
{
    public GameObject roadprefab;
    public int lastz = 0;
    public int length;
    public static UnityEvent zeroposition = new UnityEvent();
   
    public int size;
    public int renderdist;
    public int blocksperloop;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.endless)
        {
            if (length >= 300)
            {
                SpawnerScript.spawn = false;

                EndSceneHandler.kills = Player.kills;
                EndSceneHandler.time = Player.time;
                FadetoBlackthing.startanim();

            }
            else if (length >= 290)
            {
                SpawnerScript.spawn = false;
            }
            
        }
        if (lastz >= size * (blocksperloop - renderdist-2))
        {
            SpawnerScript.spawn = false;
        }    
        if (lastz > size * blocksperloop)
        {
            lastz = -size;
            SpawnerScript.spawn = false;
            for (int i = -1; i < 5; i++)
            {
                createobj();
            }

           
            zeroposition.Invoke();
        }
        if (lastz - Player.rbref.transform.position.z < renderdist * size)
        {
            createobj();
        }

    }

    private void createobj()
    {
        lastz += size;
        Instantiate(roadprefab, new Vector3Int(0, 0, lastz), Quaternion.identity, this.transform);
        length++;
    }
}
