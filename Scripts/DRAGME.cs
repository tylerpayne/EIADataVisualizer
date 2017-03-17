using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DRAGME : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler {

	public Transform dragme;
	public float speed;

	private Image image;
	private bool dragging;
	private PointerEventData lastEventData;

	// Use this for initialization
	void Start () {
		dragging = false;
		image = GetComponent<Image> ();
		Color newColor = image.color;
		newColor.a = 0.75f;
		image.color = newColor;
	}
	
	// Update is called once per frame
	void Update () {
		if (dragging) {
			Vector3 r = dragme.right;
			Vector3 u = dragme.up;
			Vector3 pos = dragme.position;
			pos += speed*lastEventData.delta.x * -r;
			pos += speed*lastEventData.delta.y * u;
			dragme.position = pos;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		Color newColor = image.color;
		newColor.a = 1f;
		image.color = newColor;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Color newColor = image.color;
		newColor.a = 0.75f;
		image.color = newColor;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		lastEventData = eventData;
		dragging = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		lastEventData = null;
		dragging = false;
	}


}
