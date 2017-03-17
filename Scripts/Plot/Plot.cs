using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Plot : MonoBehaviour {

	public enum PlotState
	{
		SeriesSettings,
		Drawn,
		Lock
	}

	public PlotState state = PlotState.SeriesSettings;

	public GameObject rotator;
	public GameObject seriesSettings;
	public GameObject axes;
	public GameObject pointType;

	public Text xlabel, xmax, xmin, ylabel, ymax, ymin;

	public RectTransform ab2point, at2point, bt2point;

	public ListItem seriesAItem;
	public ListItem seriesBItem;

	public Series seriesA = null;
	public Series seriesB = null;

	private bool isDataInteracting = false;
	public float doubleClickTimeout = 1.0f;
	private float doubleClickTimer = 0f;

	private Vector3 lastCamRot;

	private int dataLoaded = 0;

	private string series_url = "http://api.eia.gov/series/?api_key=9094bdae34fd5f528c4c1c2be5286186&series_id=";

	// Use this for initialization
	void Start () {
		seriesA = new Series ();
		seriesB = new Series ();
		axes.SetActive (false);
		seriesSettings.SetActive (true);
		rotator.SetActive (false);
		lastCamRot = Vector3.zero;
	}


	public void ShowRulers(UIPoint p)
	{
		Vector3 pos = p.transform.localPosition;
		ab2point.localPosition = new Vector3(pos.x/2f - 250f,pos.y ,pos.z/2f - 250f);
		bt2point.localPosition = new Vector3 (pos.x / 2f - 250f, (pos.y) / 2f - 250f, pos.z);
		at2point.localPosition = new Vector3 (pos.x, pos.y / 2f - 250f, pos.z / 2f - 250f);

		ab2point.sizeDelta = new Vector2 (pos.x + 500, pos.z + 500);
		bt2point.sizeDelta = new Vector2 (pos.x + 500, 500 + pos.y);
		at2point.sizeDelta = new Vector2 (pos.z + 500, 500 + pos.y);

		ab2point.gameObject.SetActive (true);
		bt2point.gameObject.SetActive (true);
		at2point.gameObject.SetActive (true);
	}

	public void HideRulers()
	{
		ab2point.gameObject.SetActive (false);
		bt2point.gameObject.SetActive (false);
		at2point.gameObject.SetActive (false);
	}

	public void dataInteractionStart()
	{
		isDataInteracting = true;
	}

	public void dataInteractionStop()
	{
		isDataInteracting = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (state == PlotState.SeriesSettings && !seriesSettings.activeSelf) {
			axes.SetActive (false);
			seriesSettings.SetActive (true);
		}
		if (state == PlotState.Drawn && seriesSettings.activeSelf) {
			if (dataLoaded == 2) {
				seriesSettings.SetActive (false);

				Debug.Log ("Series A Count: " + seriesA.data.Count + " Series B Count: " + seriesB.data.Count);
				xlabel.text = seriesA.name + " (" + seriesA.data [0].yunit + ")";
				xmin.text = seriesA.ymin.ToString();
				xmax.text = seriesA.ymax.ToString();
				ylabel.text = seriesB.name + " (" + seriesB.data [0].yunit + ")";
				ymin.text = seriesB.ymin.ToString();
				ymax.text = seriesB.ymax.ToString();
				for (int i = 0; i < seriesA.data.Count; i++) {
					Point a = seriesA.data [i];
					for (int j = 0; j < seriesB.data.Count; j++) {
						Point b = seriesB.data [j];
						if (a.x == b.x) {
							GameObject pGO = Instantiate (pointType);
							pGO.transform.SetParent (axes.transform);
							UIPoint pUIP = pGO.GetComponent<UIPoint> ();
							pUIP.plot = this;
							pUIP.pointA = a;
							pUIP.pointB = b;
							pUIP.Position ();
							pGO.SetActive (true);
						}
					}
				}
				axes.SetActive (true);
			}
		}
		if (state == PlotState.Lock) {
			Vector3 camrot = Camera.main.transform.rotation.eulerAngles;
			transform.Rotate (-2f* (camrot - lastCamRot));
			lastCamRot = camrot;
		}

		if (axes.activeSelf) {
			bool triggering = Input.GetMouseButtonDown(0);
			if (triggering) {
				if (doubleClickTimer > 0) {
					//Debug.Log (doubleClickTimer);
					DoubleClick ();
					doubleClickTimer = 0f;
				} else {
					doubleClickTimer = doubleClickTimeout;
				}
			}
		}
		doubleClickTimer -= 0.1f;
	}

	private void DoubleClick()
	{
		rotator.SetActive (!rotator.activeSelf);
	}



	public void GoLockMode()
	{
		rotator.SetActive (false);
		transform.SetParent (Camera.main.transform);
		transform.position = Vector3.zero;
		transform.localPosition = new Vector3 (0, 0, 500);
		transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
		transform.rotation = Quaternion.identity;
		lastCamRot = Camera.main.transform.rotation.eulerAngles;
		state = PlotState.Lock;
	}

	public IEnumerator DownloadSeries(Series s)
	{
		WWW eia = new WWW (series_url + s.series_id);
		yield return eia;
		if (eia.isDone) {

			s.rawJson = JSON.Parse(eia.text);
			s.name = s.rawJson ["series"][0] ["name"];
			s.description = s.rawJson ["series"][0] ["description"];

			JSONArray rawData = s.rawJson ["series"] [0] ["data"].AsArray;
			float ymin = float.MaxValue;
			float ymax = float.MinValue;
			float xmin = float.MaxValue;
			float xmax = float.MinValue;
			for (int i = 0; i < rawData.Count; i++) {
				JSONNode thisNode = rawData [i];
				Point p = new Point ();
				p.x = thisNode [0].AsFloat;
				p.y = thisNode [1].AsFloat;
				if (p.y > ymax) {
					ymax = p.y;
				}
				if (p.y < ymin)
				{
					ymin = p.y;
				}
				if (p.x > xmax) {
					xmax = p.x;
				}
				if (p.x < xmin)
				{
					xmin = p.x;
				}
				p.series = s;
				p.yunit = s.rawJson ["series"] [0] ["units"];
				s.data.Add (p);
			}
			s.ymax = ymax;
			s.ymin = ymin;
			s.xmax = xmax;
			s.xmin = xmin;
			dataLoaded++;
			Debug.Log ("Downloaded Series " + dataLoaded);
		
		}

	}
}
