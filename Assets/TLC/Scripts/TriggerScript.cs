using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TriggerScript : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {

	//[Range(0.0f, 1.0f)]
	//public int JoystickNumber;
	public int pos;

	public void OnPointerDown(PointerEventData ped)
	{
		pos = 1;
	}

	public void OnPointerUp(PointerEventData ped)
	{
		pos = 0;
	}
}