using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadetoBlackthing : MonoBehaviour
{

    public static bool animate;
    public Image fade;
    // Start is called before the first frame update
    void Start()
    {
        animate = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (animate)
        {
            fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, fade.color.a + (0.3f * Time.deltaTime));

            if (fade.color.a >= 1)
            {
                SceneChan.change("EndScene");
            }
        }
    }

    public static void startanim()
    {
        animate = true;
    }
}
