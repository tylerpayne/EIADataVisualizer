using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PUSHME : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler {

	public Transform me;
	public float speed;

	private Image image;
	private bool dragging;

	// Use this for initialization
	void Start () {
		image = GetComponent<Image> ();
		Color newColor = image.color;
		newColor.a = 0.75f;
		image.color = newColor;
	}

	void Update () {
		if (dragging) {
			Vector3 pos = speed * me.forward;
			me.position = me.position + pos;
	
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
		dragging = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		dragging = false;
	}

}
