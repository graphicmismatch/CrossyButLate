using UnityEngine;
using TMPro;
public class DeadDuck : MonoBehaviour
{

    public float walkSpeed;
    public int dir;
    private float timer;
    public TMP_Text tb;
    public bool showDialogue;
    public GameObject dialogue;
    public string[] speech;
    public Vector2 tboundspeech;
    public Vector2 bound;
    public void init(Vector2 bound) {
        this.bound = bound;
        dir = Random.Range(0,2)%2==0?-1:1;
        transform.position =(dir==1?bound.x:bound.y) * Vector3.right ;
        walkSpeed = Random.Range(10, 30f);
        transform.localScale = new Vector3(dir, 1, 1);
        tb.transform.localScale = new Vector3(-dir, 1, 1);
        tb.text = speech[Random.Range(0, speech.Length)];
        showDialogue = Random.Range(0, 2) % 2 == 0;
        dialogue.SetActive(showDialogue);
        timer = Random.Range(tboundspeech.x, tboundspeech.y);
    }

    public void Update() {
        if ((transform.position).x >=  bound.y|| (transform.position).x <= bound.x ) {
            dir *= -1;
            transform.localScale = new Vector3(dir, 1, 1);
            tb.transform.localScale= new Vector3(-dir, 1, 1);

            transform.localPosition = transform.localPosition + (Vector3.right * dir * walkSpeed*1.5f);
        }

       
        if (timer <= 0)
        {
            tb.text = speech[Random.Range(0, speech.Length)];
            showDialogue = !showDialogue;
            dialogue.SetActive(showDialogue);
            timer = Random.Range(tboundspeech.x, tboundspeech.y);
        }
        else { 
            timer -= Time.deltaTime;
        }

        
    }

    private void LateUpdate()
    {
        transform.Translate(Vector3.right * dir * walkSpeed * Time.deltaTime);
    }
}
