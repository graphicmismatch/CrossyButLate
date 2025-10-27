using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ducks : MonoBehaviour
{
    private Rigidbody rb;
    public bool goingright;
    //11.75
    public float speed;
    private SpriteRenderer tsprite;
    public AudioClip quack;
    // Start is called before the first frame update
    void Start()
    {
        goingright = Random.Range(0, 2) == 1;
        tsprite = this.GetComponent<SpriteRenderer>();
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (goingright)
        {
            tsprite.flipX = false;
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            tsprite.flipX = true;
            transform.Translate(-Vector3.right * speed * Time.deltaTime);

        }

        if (Mathf.Abs(GameManager.roadWidth - Mathf.Abs(this.transform.position.x)) <= 0.2f)
        {
            goingright = !goingright;
        }
    }

    public void duckhit()
    {
        rb.isKinematic = false;
        Vector3 imp = Player.rbref.linearVelocity;
        imp = new Vector3(imp.x + Random.Range(-0.6f, 0.6f), (imp.y + 2) * 3f, imp.z) * 2f;
        CameraShake.sc.shake(0.2f);

        AudioSource.PlayClipAtPoint(quack, this.transform.position);

        rb.AddForce(imp, ForceMode.Impulse);

        Player.t -= 0.1f;

        StartCoroutine(dest());
        DeadDuckManager.duckKilled();
        transform.tag = "Untagged";
    }

    public IEnumerator dest()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
}
