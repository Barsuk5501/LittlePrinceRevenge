using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    GameObject cam;
    Vector3 gravityPoint;
    Asteroids asteroids;
    Rigidbody rb;
    float speed;
    Vector3 dir;
    
    void Start(){
        cam = GameObject.FindWithTag("MainCamera");
        asteroids = GameObject.Find("Asteroids").GetComponent<Asteroids>();
        rb = gameObject.GetComponent<Rigidbody>();
        gravityPoint = asteroids.gravityPoint;
        speed = asteroids.asteroidSpeed;
        dir = (transform.position - gravityPoint).normalized;
        rb.velocity = -dir*speed;
    }

    void Update(){
        Vector3 fr = Vector3.Cross(cam.transform.forward, transform.up);
		transform.rotation = Quaternion.LookRotation(Vector3.Cross(fr, transform.up), transform.up);

        rb.AddForce(-dir*asteroids.asteroidAcc);
        
    }

    void OnTriggerEnter(Collider col){
        
        if (col.gameObject.tag=="Ground"){
            Destroy(this.gameObject);
            asteroids.handleCollision(transform.position);
        }
    }
}
