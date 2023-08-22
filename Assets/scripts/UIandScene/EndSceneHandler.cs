using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EndSceneHandler : MonoBehaviour
{
    public static int kills;
    public static float time;

    public TMP_Text killcount;
    public TMP_Text tmr;
    public GameObject altpfp;
    public GameObject blood;
    // Start is called before the first frame update
    void Start()
    {
        killcount.text = "Kills: " + kills;
        tmr.text = "Time: " + ((Mathf.Round(time * 10)) / 10f) + "s";


        blood.SetActive(kills >= 25);
        altpfp.SetActive(time >= 150);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
