using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject explodeyThing;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }
    public void explode()
    {
        Vector3 imp = Player.rbref.linearVelocity;
        imp = new Vector3(imp.x + Random.Range(-0.6f, 0.6f), (imp.y + 2) * 3f, imp.z) * 0.7f;
        rb.AddForce(imp, ForceMode.Impulse);
        CameraShake.sc.shake(0.5f);
        explodeyThing.SetActive(true);
        transform.tag = "Untagged";
        StartCoroutine(dest());
    }

    public IEnumerator dest()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
}
