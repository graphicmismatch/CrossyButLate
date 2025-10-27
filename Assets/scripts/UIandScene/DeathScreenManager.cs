using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DeathScreenManager : MonoBehaviour
{
    public static float time;
    public static int kills;
    public TMP_InputField uname;
    public GameObject endlessModeDS;
    public GameObject defaultDS;
    public Button endlessSubmit;

    public TMP_Text commonResult;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string res = string.Format("Your reign of terror lasted {0:F1}s.", time);
        if (kills >= 50) {
            int n = Random.Range(1, (int)kills / 2);

            res += $"\nYou killed {kills - n} innocent ducks.";
            res += $"\nYou killed {n} serial stabbing ducks.";
            res += $"\nYou reduced the duck population by {kills}.";
        }
        else {
            res += $"\nYou killed {kills} innocent ducks.";
        }
        res += "\nBut now you are dead.";
        commonResult.text = res;
        if (GameManager.instance != null && GameManager.endless)
        {
            endlessModeDS.SetActive(true);
            defaultDS.SetActive(false);
            uname.text = GameManager.username;
            if (GameManager.username.Trim() == "")
            {
                endlessSubmit.interactable = false;
            }


        }
        else {
            endlessModeDS.SetActive(false);
            defaultDS.SetActive(true);

        }
    }


    public void updateUsername(string s) {
        endlessSubmit.interactable= s.Trim() != "";
        GameManager.username = s;
    }

    public void submit() {
        if (GameManager.endless) {
            GameManager.instance.trySetScore(kills, time);
        }
        SceneManager.LoadScene("MainMenu");
    }
}
