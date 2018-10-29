using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public GameObject Player;
	public Vector3 dir;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		dir = (Player.transform.eulerAngles - transform.eulerAngles).normalized;
		transform.Rotate(new Vector3(dir.x*0.2f, dir.y*0.2f, dir.z*0.2f));
	}
}
