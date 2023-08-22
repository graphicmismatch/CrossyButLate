using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    private float jumpHeight;
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
        
    }

    public float calcJumpH(float t)
    {
        return (-4 * jumpHeight * Mathf.Pow((t - 0.5f), 2) + jumpHeight);
    }
}
