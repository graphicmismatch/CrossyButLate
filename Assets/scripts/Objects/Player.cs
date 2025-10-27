using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Player : MonoBehaviour
{

    public float acceleration;
    public float speed;
    public static float t;
    public static float time;
    public float maxspeed;
    public Rigidbody rb;
    public Collider coll;
    public static Rigidbody rbref;
    public Image speedmeter;
    public static int kills;
    public AudioSource engine;
    public TMP_Text killcount;
    public TMP_Text tmr;
    public bool updatedrampstate;
    public float prevspeed;
    public bool hitsum;
    public bool phit;
    public Vector3 ogGrav;
    public bool onramp = false;
    public AudioClip tyrepop;
    public float maxHealth;
    public float health;
    public Image healthBar;
    public bool exploded;

    public bool dead;
    // Start is called before the first frame update
    void Start()
    {
        t = 0f;
        time = 0f;
        kills = 0;
        rbref = rb;
        roadgen.zeroposition.AddListener(zeroposition);
        SpawnerScript.spawn = true;
        phit = false;
        onramp = false;
        ogGrav = Physics.gravity;
        changeHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        hitsum = false;
        updatedrampstate = false;
        tmr.text = ((Mathf.Round(time * 10)) / 10f) + "s";
        killcount.text = "" + kills;
        //input
        float inp = Input.GetAxis("Horizontal");
        //force calc
        float sp = clamp(0, maxspeed * Time.deltaTime, speed * Time.deltaTime + acceleration * t * Time.deltaTime);
        //fake physics based rotation
        this.transform.eulerAngles = new Vector3(0, map(inp, -1, 1, -14, 14), 0);
        //clampypos
        if (!onramp && !exploded)
        {
            this.transform.position = new Vector3(this.transform.position.x, clamp(0.3f, 0.35f, this.transform.position.y), this.transform.position.z);
        }



        if (this.transform.position.y > 4.6f)
        {
            Physics.gravity = ogGrav*2;
        }
        else
        {
            Physics.gravity = ogGrav;
        }
        //addforce
        rb.AddForce(new Vector3(inp * speed * 1.7f * Time.deltaTime*(map(rb.linearVelocity.z, 0, 125, 1f, 1.1f)), 0, sp), ForceMode.VelocityChange);
        RaycastHit[] hits;
        hits = Physics.BoxCastAll(coll.bounds.center, coll.bounds.size / 2, transform.forward, Quaternion.identity, 0.5f);
        if (hits.Length > 0)
        {
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.tag == "Duck")
                {
                    if (hit.transform.gameObject.GetComponent<Rigidbody>().isKinematic)
                    {
                        hit.transform.gameObject.GetComponent<Ducks>().duckhit();
                        kills++;
                        hitsum = true;
                        changeHealth(2);
                    }
                }
                else if (hit.transform.tag == "Spike")
                {
                    rb.AddForce(-rb.linearVelocity * 2, ForceMode.Impulse);
                    t /= 2;
                    Destroy(hit.transform.gameObject);
                    hitsum = true;
                    AudioSource.PlayClipAtPoint(tyrepop, Camera.main.transform.position);
                    changeHealth(-10);
                }
                else if (hit.transform.tag == "Ramp")
                {
                    onramp = true;
                    updatedrampstate = true;
                }
                else if (hit.transform.tag == "Barrel")
                {
                    hit.transform.gameObject.GetComponent<Barrel>().explode();
                    rb.AddForce(new Vector3(Random.Range(-5f,5f),4.3f,-65), ForceMode.VelocityChange);
                    t /= 2;
                    exploded = true;
                    updatedrampstate = true;
                    changeHealth(-80);
                }

            }
        }

        if ((exploded||onramp) && !updatedrampstate)
        {
            if (this.transform.position.y <= 0.35f)
            {
                onramp = false;
                exploded = false;
            }
            
        }


        if (125 - rb.linearVelocity.z > 5)
        {
            speedmeter.fillAmount = map(rb.linearVelocity.z, 0, 125, 0, 1f);

        }
        else
        {
            speedmeter.fillAmount = 1;
        }

        engine.pitch = map(speedmeter.fillAmount, 0, 1, 0, 3f);


        if (!hitsum && !phit && rb.linearVelocity.z < 3f)
        {
            rb.AddExplosionForce(10, new Vector3(transform.position.x, this.transform.position.y - 0.5f, this.transform.position.z), 3);
            //rb.AddForce(0, 0, prevspeed, ForceMode.VelocityChange);
            print("yeppers");
        }

        prevspeed = rb.linearVelocity.z;
        phit = hitsum;

        t += Time.deltaTime;
        time += Time.deltaTime;
    }

    public static float clamp(float a, float b, float val)
    {
        if (val < a)
        {
            return a;
        }
        else if (val > b)
        {

            return b;
        }
        else
        {
            return val;
        }
    }

    public static float map(float value, float start1, float stop1, float start2, float stop2)
    {
        return start2 + (stop2 - start2) * ((value - start1) / (stop1 - start1));
    }

    public void changeHealth(float delta) {
        health = Mathf.Clamp(health + delta, 0, maxHealth);
        if (health <= 0 || Mathf.Approximately(health, 0)) {
            die();
        }
        float curr = healthBar.fillAmount;
        LeanTween.value(this.gameObject, (float x, object o) => { healthBar.fillAmount = x; }, curr,health/maxHealth,0.2f);
    }
    public void die() {
        if (GameManager.endless)
        {
            if (GameManager.instance != null)
            {
                DeathScreenManager.time = time;
                DeathScreenManager.kills = kills;
            }
        }
            FadetoBlackthing.startanim();
    }
    public void zeroposition()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        StartCoroutine(restartspawn());
    }

    public IEnumerator restartspawn()
    {
        yield return new WaitForEndOfFrame();
        SpawnerScript.spawn = true;
    }
}