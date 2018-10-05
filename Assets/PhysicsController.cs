using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsController : MonoBehaviour {
	public Vector3 cot;
	public float angleX;
	public float angleY;
	public Vector3 dir;
	public GameObject gravityBody;
	public GameObject player;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.dir = ( player.transform.position - gravityBody.transform.position ).normalized;
		this.dir = new Vector3(this.dir.x*100, this.dir.y*100, this.dir.z*100);
		//
		
		Debug.DrawLine(Vector3.zero, new Vector3(this.dir.x,this.dir.y,0));
		Debug.DrawLine(Vector3.zero, new Vector3(Vector3.up.x*100, Vector3.up.y*100, Vector3.up.z*100));
		if (dir.z > 0){
			this.angleX = Vector3.Angle(Vector3.up, new Vector3(0,this.dir.y,this.dir.z));
		}
		if (dir.z  < 0){
			this.angleX = -Vector3.Angle(Vector3.up, new Vector3(0,this.dir.y,this.dir.z));
		}
		if (dir.x > 0){
			this.angleY = Vector3.Angle(Vector3.up, new Vector3(this.dir.x,this.dir.y,0));
		}
		if (dir.x  < 0){
			this.angleY = -Vector3.Angle(Vector3.up, new Vector3(this.dir.x,this.dir.y,0));
		}
	 	this.player.transform.eulerAngles = new Vector3(this.angleX, 0, -this.angleY);
		//player.GetComponent<Rigidbody>().AddForce(this.dir);

	
		if (Input.GetKey(KeyCode.W)){
			player.transform.position += new Vector3(0,-Mathf.Sin(Mathf.Deg2Rad*this.angleX),Mathf.Cos(Mathf.Deg2Rad*this.angleX));
		}
		if (Input.GetKey(KeyCode.A)){
			player.transform.position += new Vector3(-Mathf.Cos(Mathf.Deg2Rad*this.angleY),Mathf.Sin(Mathf.Deg2Rad*this.angleY),0);
		}
	}
}
