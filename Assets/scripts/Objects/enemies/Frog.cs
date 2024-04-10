using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    public float jumpHeight;
    public static Vector2 jumpHeightbounds;
    public float speedX;
    public int jumps;
    private float jumpDistance;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x+1,calcJumpH(this.transform.position.x + 1),this.transform.position.z); 
    }

    public float calcJumpH(float t)
    {
        return jumpHeight * Mathf.Abs(Mathf.Sin(t * Mathf.PI * jumps / (2 * GameManager.roadWidth)));
    }
}
