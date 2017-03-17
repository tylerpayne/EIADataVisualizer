using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class DESTROYMEBUTTON : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IPointerDownHandler {

	public GameObject me;

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
		Destroy (me);
	}

}
