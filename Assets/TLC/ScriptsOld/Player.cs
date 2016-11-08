using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float speed = 10f;

    private float lastSynchronizationTime = 0f;
    private float syncDelay = 0f;
    private float syncTime = 0f;
    private Vector3 syncStartPosition = Vector3.zero;
    private Vector3 syncEndPosition = Vector3.zero;

	//Movimento
	private Rigidbody rb;
	private float h, v;
	private Vector3 movementVector;
	private bool correndo;

	//Animacao
	private Animator animator;

    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        Vector3 syncPosition = Vector3.zero;
        Vector3 syncVelocity = Vector3.zero;
        if (stream.isWriting)
        {
            syncPosition = GetComponent<Rigidbody>().position;
            stream.Serialize(ref syncPosition);

            syncPosition = GetComponent<Rigidbody>().velocity;
            stream.Serialize(ref syncVelocity);
        }
        else
        {
            stream.Serialize(ref syncPosition);
            stream.Serialize(ref syncVelocity);

            syncTime = 0f;
            syncDelay = Time.time - lastSynchronizationTime;
            lastSynchronizationTime = Time.time;

            syncEndPosition = syncPosition + syncVelocity * syncDelay;
            syncStartPosition = GetComponent<Rigidbody>().position;
        }
    }

    void Awake()
    {
		rb = GetComponent<Rigidbody>();
		animator = transform.FindChild ("Model").GetComponent<Animator> ();
		correndo = false;
		lastSynchronizationTime = Time.time;
    }

    void Update()
    {
		ChangeMoveAnimation ();

		if (GetComponent<NetworkView>().isMine)
        {
			InputMovement();
            InputColorChange();
        }
        else
        {
            SyncedMovement();
        }
    }


    private void InputMovement() {
		h = Input.GetAxisRaw ("Horizontal");
		v = Input.GetAxisRaw ("Vertical");

		movementVector = new Vector3 (h, 0, v);
		movementVector.Normalize ();

		if (!correndo) {
			rb.AddForce (movementVector * (speed));
		} else {
			rb.AddForce (movementVector * (speed * 1.3f));
		}

		if (Input.GetKey (KeyCode.LeftShift)) {
			correndo = true;
		} else {
			correndo = false;
		}
//        if (Input.GetKey(KeyCode.W))
//            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + Vector3.forward * speed * Time.deltaTime);
//
//        if (Input.GetKey(KeyCode.S))
//            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position - Vector3.forward * speed * Time.deltaTime);
//
//        if (Input.GetKey(KeyCode.D))
//            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + Vector3.right * speed * Time.deltaTime);
//
//        if (Input.GetKey(KeyCode.A))
//            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position - Vector3.right * speed * Time.deltaTime);
    }

    private void SyncedMovement()
    {
        syncTime += Time.deltaTime;

        GetComponent<Rigidbody>().position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
    }


    private void InputColorChange()
    {
        if (Input.GetKeyDown(KeyCode.R))
            ChangeColorTo(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
    }

    [RPC] void ChangeColorTo(Vector3 color) {
        GetComponent<Renderer>().material.color = new Color(color.x, color.y, color.z, 1f);

        if (GetComponent<NetworkView>().isMine)
            GetComponent<NetworkView>().RPC("ChangeColorTo", RPCMode.OthersBuffered, color);
    }

	[RPC] void ChangeMoveAnimation() {
		if (movementVector.magnitude > 0) {
			if (!correndo) {
				animator.SetBool ("Moving", true); //trocar para triger depois
			} else {
				//animacao de corrida
			}
		} else {
			animator.SetBool ("Moving", false); //colocar animacao idle aqui
		}

		if (GetComponent<NetworkView>().isMine)
			GetComponent<NetworkView>().RPC("ChangeMoveAnimation", RPCMode.OthersBuffered);
	}
}
