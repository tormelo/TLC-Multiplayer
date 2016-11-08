using UnityEngine;
using System.Collections;

public class VirtualController : MonoBehaviour {


	public GameObject RightTrigger;
	public GameObject RightJoystick;

	// Update is called once per frame
	void Update () {
		if (SaveSystem.current.miraAutomatica) 
		{
			if (!RightTrigger.activeSelf && RightJoystick.activeSelf) 
			{
				RightTrigger.SetActive (true);
				RightJoystick.SetActive (false);
			}
		} 
		else 
		{
			if (RightTrigger.activeSelf && !RightJoystick.activeSelf) 
			{
				RightTrigger.SetActive (false);
				RightJoystick.SetActive (true);
			}
		}
	}
}
