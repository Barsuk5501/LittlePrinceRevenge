using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public GameObject player;
	public float speed;
	Vector3 pPos;
	Vector3 pos;

	void FixedUpdate () {
		pPos = player.transform.position;
		pos = transform.position;
		Vector3 axis = Vector3.Cross(pPos, pos);
		transform.RotateAround(Vector3.zero, -axis, speed * Time.deltaTime);
	}
}
