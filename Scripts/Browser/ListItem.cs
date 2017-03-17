using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ListItem : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler {

	public string category_id = null;
	public string series_id = null;
	public string name = null;

	public Text text;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (category_id != null) {
			Browser.instance.GoToCategory (category_id);
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		Image i = GetComponent<Image> ();
		Color c = i.color;
		c.a = 1f;
		i.color = c;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Image i = GetComponent<Image> ();
		Color c = i.color;
		c.a = 0.75f;
		i.color = c;
	}
}
