using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DamageTextController : MonoBehaviour
{
    public float destroyTime = 0.6f;
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector3(0, 600, 0));
        StartCoroutine(DestroyObject());
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(DestroyObject());
    }

    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(this.gameObject);
    }
}