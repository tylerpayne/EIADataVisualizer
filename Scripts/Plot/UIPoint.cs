using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIPoint : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {
	public Plot plot;
	public Point pointA;
	public Point pointB;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		transform.LookAt (Vector3.zero);
		transform.Rotate (Vector3.up * 180);
	}

	public void Position()
	{
		float timemin = pointA.series.xmin;
		if (pointB.series.xmin < timemin) {
			timemin = pointB.series.xmin;
		}
		float timemax = pointA.series.xmax;
		if (pointB.series.xmax > timemax) {
			timemax = pointB.series.xmax;
		}

		float tinc = 1000f / (timemax - timemin);

		float amin = pointA.series.ymin;
		float amax = pointA.series.ymax;
		float ainc = 1000f / (amax - amin);

		float bmin = pointB.series.ymin;
		float bmax = pointB.series.ymax;
		float binc = 1000f / (bmax - bmin);

		Vector3 pos = new Vector3 ((pointA.y - amin) * ainc -500f, (pointA.x - timemin) * tinc - 500f, (pointB.y - bmin) * binc - 500f);
		transform.localPosition = pos;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (PointHUD.instance.hold == null) {
			plot.dataInteractionStart ();
			plot.ShowRulers (this);
			PointHUD.instance.SetActivePoint (this);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (PointHUD.instance.hold == null) {
			PointHUD.instance.Hide ();
			plot.HideRulers ();
			plot.dataInteractionStop ();
		}

	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (PointHUD.instance.hold == this) {
			PointHUD.instance.hold = null;
		} else {
			PointHUD.instance.hold = this;
		}

	}
		
}
