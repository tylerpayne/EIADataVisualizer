using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewPlotButton : MonoBehaviour, IPointerDownHandler {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		GameObject newPlot = Instantiate (ResearchEnvironment.instance.plotType);
		Vector3 pos = Browser.instance.transform.position;
		Vector3 r = Browser.instance.transform.right;
		pos = pos - Vector3.Normalize(r)*400;
		newPlot.transform.rotation = Browser.instance.transform.rotation;
		newPlot.transform.position = pos;
		newPlot.SetActive (true);
		ResearchEnvironment.instance.plots.Add (newPlot.GetComponent<Plot>());
	}
}
