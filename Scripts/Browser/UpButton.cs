using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {


	private bool holding;
	public float speed;
	public float effector;
	float y = 0;
	PointerEventData lastEventData;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (holding) {
			y += lastEventData.delta.y * effector;
			Browser.instance.scrollArea.content.Translate (new Vector3 (0, speed - y*Mathf.Sign(speed), 0));
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		holding = false;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		y = 0;
		lastEventData = eventData;
		holding = true;
	}
		
}
