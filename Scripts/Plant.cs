using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {
	GameObject cam;
	void Start(){
		cam = GameObject.FindWithTag("MainCamera");
	}
	void Update () {
		Vector3 fr = Vector3.Cross(cam.transform.forward, transform.up);
		transform.rotation = Quaternion.LookRotation(Vector3.Cross(fr, transform.up), transform.up);
	}
}
