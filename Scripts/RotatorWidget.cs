using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotatorWidget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler {

	public Vector3 axis;
	//public Vector3 aroundPoint;
	public Transform target;
	public float speed;

	private bool dragging = false;
	private PointerEventData lastEventData;


	Vector3 initialScale;
	// Use this for initialization
	void Start () {
		initialScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		if (dragging) {
			float delta = lastEventData.delta.y;
			if (axis.y != 0) {
				delta = lastEventData.delta.x;
			}
			target.Rotate(axis, delta*speed);
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		transform.localScale = new Vector3 (initialScale.x + 50f, initialScale.y, initialScale.z + 50f);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		transform.localScale = initialScale;
	}

	public void OnPointerDown(PointerEventData eventData){
		lastEventData = eventData;
		dragging = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		dragging = false;
		lastEventData = null;
	}

}
