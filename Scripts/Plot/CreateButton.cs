using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreateButton : MonoBehaviour, IPointerDownHandler {

	public Plot plot;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		plot.seriesA.series_id = plot.seriesAItem.series_id;
		plot.seriesB.series_id = plot.seriesBItem.series_id;
		plot.StartCoroutine (plot.DownloadSeries (plot.seriesA));
		plot.StartCoroutine (plot.DownloadSeries (plot.seriesB));
		plot.state = Plot.PlotState.Drawn;
	}
}
