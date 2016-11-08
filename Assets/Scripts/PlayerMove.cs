using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour {
	
	//Movimento
	private Animator animator;
	public float Speed = 10f;
	public float RunMultiplier;
	private Rigidbody rb;
	private float h, v;
	private Vector3 movementVector;
	private bool correndo;

	//Mira
	private GameObject model;
	private int layerMask = 1 << 8;
	private Vector3 HitPosition;
	//private bool hasFocus;

	void Awake () {
		rb = GetComponent<Rigidbody>();
		animator = transform.FindChild ("Model").GetComponent<Animator> ();
		correndo = false;
		model = transform.FindChild ("Model").gameObject;
//		hasFocus = true;
	}

	void Update () {
		if (isLocalPlayer) {
			InputMovement ();
			mirar ();
			animar ();
		}
	}

	void InputMovement() {
		h = Input.GetAxisRaw ("Horizontal");
		v = Input.GetAxisRaw ("Vertical");

		movementVector = new Vector3 (h, 0, v);
		movementVector.Normalize ();

		if (!correndo) {
			rb.AddForce (movementVector * (Speed));
		} else {
			rb.AddForce (movementVector * (Speed * RunMultiplier));
		}

		if (Input.GetKey (KeyCode.LeftShift)) {
			correndo = true;
		} else {
			correndo = false;
		}
	}

	void mirar(){
		/*
		Cria um raio que vai da camera ate o chao.
		A posiçao do hit desse raio e armazenada na variavel HitPosition para ser 
		posteriormente usada para rotacionar o corpo do personagem e a arma dele.
		 */
//		if (hasFocus) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
				HitPosition = new Vector3(hit.point.x, model.transform.position.y, hit.point.z);
				model.transform.LookAt(HitPosition);
				//Debug.Log (HitPosition);
			}
//		}
	}

	void animar() {
		if (movementVector.magnitude > 0) {
			animator.SetBool ("Moving", true); //trocar para triger depois
		} else {
			animator.SetBool ("Moving", false); //trocar para triger depois
		}
	}
//
//	void OnApplicationFocus(bool focus) {
//		hasFocus = focus;
//	}
}
