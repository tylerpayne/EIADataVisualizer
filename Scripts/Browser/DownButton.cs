using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DownButton : MonoBehaviour, IPointerUpHandler {


	private bool holding;
	public float speed;
	private PointerEventData lastEventData;
	private Vector2 pos;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (holding) {
			Browser.instance.scrollArea.content.Translate (new Vector3 (0, -1f * speed, 0));
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		holding = false;
		lastEventData = null;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		lastEventData = eventData;
		pos = eventData.position;
		holding = true;
	}

}
