using System;
using System.Collections;
using System.IO.Hashing;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
public class LeaderboardInteractor:MonoBehaviour
{
    public TextAsset keyText;
    public TextAsset urlText;
    public static LeaderboardInteractor instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        getFromLeaderBoard((Leaderboard x) => { print(x.users.Count); });
    }


    public void sendToLeaderBoard(string username, int score, Action<UnityWebRequest.Result> callback) { 
        StartCoroutine(sendToLeaderBoardCo(username, score, callback));
    }

    IEnumerator sendToLeaderBoardCo(string username, int score, Action<UnityWebRequest.Result> callback) {
        UnityWebRequest request = new UnityWebRequest(urlText.text+"/leaderboard", "POST");

        string data = $"{{ \"userid\":\"{(Mathf.Abs(DateTime.UtcNow.Ticks.GetHashCode()) + "|" + username)}\",\"score\":{score} }}";
        byte[] bodyRaw = Encoding.UTF8.GetBytes(data);
        request.uploadHandler  =  (UploadHandler)new UploadHandlerRaw(bodyRaw);
    
        request.SetRequestHeader("x-api-key", keyText.text);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();
        print(request.responseCode);
        print(request.error);
        print(request.result);
       
        callback.Invoke(request.result);
    }

    public void getFromLeaderBoard(Action<Leaderboard> callback) {
        StartCoroutine(getFromLeaderBoardCo(callback));
    }

    IEnumerator getFromLeaderBoardCo(Action<Leaderboard> callback) {
        UnityWebRequest request = new UnityWebRequest(urlText.text + "/leaderboard?userid=all", "GET");
        request.SetRequestHeader("x-api-key", keyText.text);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();
        print(request.responseCode);
        print(request.error);
        print(request.result);
        print(request.downloadHandler.text);
        Leaderboard lb = JsonConvert.DeserializeObject<Leaderboard>(request.downloadHandler.text);
        callback.Invoke(lb);
    }
}
