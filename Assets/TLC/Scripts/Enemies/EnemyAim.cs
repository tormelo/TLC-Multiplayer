using UnityEngine;
using System.Collections;

public class EnemyAim : MonoBehaviour {

	private Transform Player;
	private Transform Body;

	void Update () {
		Player = GameObject.Find ("Player").transform;
		Body = transform.parent.FindChild ("Body").transform;

		Vector3 pos = new Vector3 (transform.position.x, Body.position.y, transform.position.z);

		transform.position = pos;
		transform.LookAt (Player);
	}
}
