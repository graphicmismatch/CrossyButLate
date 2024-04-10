using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        roadgen.zeroposition.AddListener(zeroposition);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z - Player.rbref.transform.position.z < -50)
        {
            Destroy(this.gameObject);
        }
    }

    public void zeroposition()
    {
        if (transform.position.z > 175)
        {
            Destroy(this.gameObject);
        }
    }
}
