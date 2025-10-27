using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    public static float roadWidth = 11.75f;
    public static int highScore;
    // Start is called before the first frame update
    public static GameManager instance;
    public static bool endless;
    public static string username = "";
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { 
            Destroy(gameObject);
        }
        
    }
    void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore", 0);
    }


    public void trySetScore(int kills, float time) {

        if (username == "") {
            return;
        }
        int score = Mathf.RoundToInt(15 * time) + (30 * kills);
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("highScore", highScore);
            PlayerPrefs.Save();
            LeaderboardInteractor.instance.sendToLeaderBoard(username, score,(UnityWebRequest.Result x) => { print(x); });
        }
    }
}
