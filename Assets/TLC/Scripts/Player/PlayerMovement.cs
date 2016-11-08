using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public Animator animator;
	public float Speed;

	private Rigidbody rb;
	private float h, v;
	private Vector3 vec;
	private bool isMoving;

	void Start () {
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate () {
		if (GameObject.Find ("GameMaster").GetComponent<Master> ().estadoJogador != 1) {


			//Se está rodando em um dispositivo movel utiliza numeros do joystick virtual, do contrario usa dados do teclado
			if (SystemInfo.deviceType == DeviceType.Handheld) 
			{
				Vector2 movDirection = GameObject.Find ("Left Joystick").GetComponent<JoystickScript> ().pos;
				movDirection.Normalize ();

				h = movDirection.x;
				v = movDirection.y;

			} else 
			{
				h = Input.GetAxisRaw ("Horizontal");
				v = Input.GetAxisRaw ("Vertical");
			}

			vec = new Vector3 (h, 0, v);

			if (SaveSystem.current.selectedPerk == 1) 
			{
				rb.AddForce (vec * (Speed * 1.3f));
			} 
			else 
			{
				rb.AddForce (vec * Speed);
			}
		}
	}

	void Update (){
		if (GameObject.Find ("GameMaster").GetComponent<Master> ().estadoJogador != 1) {
			if (h != 0 || v != 0) {
				isMoving = true;
			} else {
				isMoving = false;
			}


			animator.SetBool ("Moving", isMoving);
		}
	}
}
