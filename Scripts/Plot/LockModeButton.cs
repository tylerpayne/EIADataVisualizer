using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LockModeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler {

	public Plot plot;

	private Image image;

	// Use this for initialization
	void Start () {
		image = GetComponent<Image> ();
		Color newColor = image.color;
		newColor.a = 0.75f;
		image.color = newColor;
	}
	
	// Update is called once per frame
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
		plot.GoLockMode ();
	}

	public void OnPointerUp(PointerEventData eventData)
	{

	}



}
