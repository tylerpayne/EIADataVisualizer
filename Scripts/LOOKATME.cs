using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class LOOKATME : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IPointerDownHandler {

	public Transform looker;

	private Image image;

	// Use this for initialization
	void Start () {
		image = GetComponent<Image> ();
		Color newColor = image.color;
		newColor.a = 0.75f;
		image.color = newColor;
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
		looker.LookAt (Camera.main.transform.position);
		looker.Rotate (0, 180, 0);
	}
}
