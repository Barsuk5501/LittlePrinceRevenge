using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsController : MonoBehaviour {
	Animator m_Animator;
	public GameObject player;
	Vector3 rot;
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		m_Animator = player.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Escape)){
			UnityEditor.EditorApplication.isPlaying = false;
		}
        if (Input.GetKey(KeyCode.P)){
			UnityEditor.EditorApplication.isPaused = true;
		}
		if (Input.GetKey(KeyCode.C)){
			if (Cursor.lockState == CursorLockMode.Locked){
				Cursor.lockState = CursorLockMode.None;
			} else {
				Cursor.lockState = CursorLockMode.Locked;
			}
		}	
    	if (Input.GetKeyDown(KeyCode.W)){
			m_Animator.Play("ForwardWalk");
		}
		if (Input.GetKeyDown(KeyCode.A)){
			m_Animator.Play("RightWalk");
		}
		if (Input.GetKeyDown(KeyCode.S)){
			m_Animator.Play("BackWalk");
		}
		if (Input.GetKeyDown(KeyCode.D)){
			m_Animator.Play("LeftWalk");
		}
		
		rot = new Vector3(0,0,0);
		if (Input.GetKey(KeyCode.W)){
			rot = new Vector3(0.2f, rot.y, rot.z);;	
		}
		if (Input.GetKey(KeyCode.A)){
			rot = new Vector3(rot.x, rot.y, 0.2f);
		}
		if (Input.GetKey(KeyCode.S)){
			rot = new Vector3(-0.2f, rot.y, rot.z);
		}
		if (Input.GetKey(KeyCode.D)){
			rot = new Vector3(rot.x, rot.y, -0.2f);			
		} 
		if (
			!Input.GetKey(KeyCode.W) &&
			!Input.GetKey(KeyCode.A) &&
			!Input.GetKey(KeyCode.S) &&
			!Input.GetKey(KeyCode.D)
		) {
			m_Animator.Play("Idle");
		}
		rot = rot.normalized;
		
		
	}
	void FixedUpdate() {
		transform.Rotate (new Vector3(
			rot.x/5, 
			Cursor.lockState == CursorLockMode.Locked ? Input.GetAxis("Mouse X")*Time.deltaTime*20 : 0, 
			rot.z/5
			)
		);
	}
}
