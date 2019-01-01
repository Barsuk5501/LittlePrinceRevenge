using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsController : MonoBehaviour {
	public GameObject sph;
	public Animator m_Animator;
	[Range(0,20)]
	public float speed = 8;

	void Start () {
		RenderSettings.skybox.SetFloat("_Rotation", Time.time*100);
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Escape)){
			UnityEditor.EditorApplication.isPlaying = false;
		}
        if (Input.GetKey(KeyCode.P)){
			Cursor.lockState = CursorLockMode.None;
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
			m_Animator.Play("Run");
		}
		if (Input.GetKeyDown(KeyCode.A)){
			m_Animator.Play("Left");
		}
		if (Input.GetKeyDown(KeyCode.S)){
			m_Animator.Play("Run_Back");
		}
		if (Input.GetKeyDown(KeyCode.D)){
			m_Animator.Play("Right");		
		}
		
		//if (Input.GetMouseButtonDown(0)){
			//m_Animator.Play("Attack");		
		//}
		
		if (
			!Input.GetKey(KeyCode.W) &&
			!Input.GetKey(KeyCode.A) &&
			!Input.GetKey(KeyCode.S) &&
			!Input.GetKey(KeyCode.D)
		) {
			m_Animator.Play("Idle");
		}	
	}
	void FixedUpdate() {
		sph.transform.RotateAround(
			Vector3.zero, 
			-transform.up, 
			Cursor.lockState == CursorLockMode.Locked ? Input.GetAxis("Mouse X")*Time.deltaTime*20 : 0
		);
		Vector3 axis = new Vector3(0,0,0);
		if (Input.GetKey(KeyCode.W)){	
			axis-=transform.right;
		}
		if (Input.GetKey(KeyCode.A)){
			axis+=Vector3.Cross(transform.up,transform.right);

		}
		if (Input.GetKey(KeyCode.S)){
			axis+=transform.right;

		}
		if (Input.GetKey(KeyCode.D)){
			axis-=Vector3.Cross(transform.up,transform.right);
				
		}  
		sph.transform.RotateAround(Vector3.zero, -axis, speed/30);	
	}
}
