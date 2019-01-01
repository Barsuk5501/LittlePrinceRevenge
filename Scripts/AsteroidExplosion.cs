using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidExplosion : MonoBehaviour
{
    
    float Timer = 0;
    void Update()
    {
        Timer += Time.deltaTime;
        if(Timer>5){
            Destroy(this.gameObject);
        }
    }
}
