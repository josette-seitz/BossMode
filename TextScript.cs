using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextScript : MonoBehaviour {
    public GameObject text1, text2, text3, text4;
    public GameObject leftPlanet;
    public GameObject rightPlanet;
    public GameObject explodeObj;
    public GameObject starsExplode;
    //force of explosion that sends GameObjects
    private float force = 10.0f;
    //radius of our explosion
    private float radius = 0.50f;
    private float upForce = 0f;
    private float time;
    private Rigidbody leftRig;
    private Rigidbody rightRig;
    private float speed = 5.5f;

	void Start () {

        leftRig = leftPlanet.AddComponent<Rigidbody>().GetComponent<Rigidbody>();
        rightRig = rightPlanet.AddComponent<Rigidbody>().GetComponent<Rigidbody>();
        leftRig.useGravity = false;
        rightRig.useGravity = false;
    }

    void Explosion()
    {
        //explosion positon
        Vector3 explodePosition = explodeObj.transform.position;
        Collider[] detectColliders = Physics.OverlapSphere(explodePosition, radius);
        foreach(Collider dc in detectColliders)
        {
            Rigidbody rb = dc.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(force, explodePosition, radius, upForce, ForceMode.Impulse);
                Destroy(leftPlanet);
                Destroy(rightPlanet);
                starsExplode.SetActive(true);

            }
        }
    }

	void FixedUpdate () {
        time = Time.time;
        if(time > 1)
        {
            text1.SetActive(true);
        }
        if(time > 1.1)
        {
            text2.SetActive(true);
        }
        if(time > 2)
        {
            text3.SetActive(true);
            if(leftRig != null && rightRig != null)
            {
                leftRig.velocity = transform.right * speed;
                rightRig.velocity = -transform.right * speed;
                Explosion();
            }
        }
        if(time > 2.5)
        {
            text4.SetActive(true);
        }

	}
}
