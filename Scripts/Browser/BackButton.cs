﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackButton : MonoBehaviour, IPointerUpHandler {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		Browser.instance.GoBack ();
	}
}
