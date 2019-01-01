using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{   

    
    public List<GameObject> asteroids = new List<GameObject>();
    public GameObject explosion;
    public Vector3 gravityPoint = new Vector3(0,0,0);
    public Transform target;

    [Range(1,20)]
    public float delayValue = 3;

     [Range(0,5)]
    public float delayRandom = 0;
    

    [Range(0,20)]
    public float heightRandom = 0;

    [Range(0,100)]
    public float heightValue = 10;

    [Range(0,360)]
    public float angleRandom = 15;

    [Range(0,100)]
    public float asteroidSpeed = 10;

   [Range(0,100)]
    public float asteroidAcc = 100;
    float Timer = 0;
    float delay = 0;

    void Start() {
        delay = delayValue;
    }
    void Update()
    {
        Timer+=Time.deltaTime;
        if (Timer>delay){
            Vector3 pos = Quaternion.AngleAxis(Random.Range(-angleRandom, angleRandom), Random.insideUnitCircle) * target.position;
            Vector3 dir = (pos-gravityPoint).normalized;
            Vector3 height = dir*(Random.Range(-heightRandom, heightRandom)+heightValue);
            GameObject inst = (GameObject) Instantiate(asteroids[Random.Range(0, asteroids.Count)], pos+height,  Quaternion.identity);
            inst.transform.parent = transform;

            Timer = 0;
            delay = delayValue+Random.Range(0, delayRandom);
        }   
    }

    public void handleCollision(Vector3 pos){
        GameObject inst = (GameObject) Instantiate(explosion, pos,  Quaternion.identity);
        inst.transform.parent = transform;
    }
}
