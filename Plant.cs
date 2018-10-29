using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {
	public GameObject Player;
	public float angle;
	public Vector3 pos;
	public GameObject cam;
	// Use this for initialization
	void Start () {
		angle = transform.localEulerAngles.y;
	}
	
	// Update is called once per frame
	void Update () {
		pos = new Vector3(Player.transform.forward.x, 0, Player.transform.forward.z).normalized;
		angle = Vector3.Angle(pos, Vector3.forward);
		if (pos.x<0)
			angle = -angle;
		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, angle, transform.localEulerAngles.z);
	}
}
