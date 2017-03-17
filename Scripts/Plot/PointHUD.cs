using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointHUD : MonoBehaviour {
	public static PointHUD instance;

	public GameObject contents;
	public Text time;
	public Text A;
	public Text B;

	public Text ATitle;
	public Text BTitle;

	public UIPoint hold;

	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
			contents.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		//transform.LookAt (Vector3.zero);
		//transform.Rotate (Vector3.up * 180);
	}

	public void Show()
	{
		contents.SetActive (true);
	}

	public void Hide()
	{
		contents.SetActive (false);
	}

	public void SetActivePoint(UIPoint p)
	{
		//transform.position = p.transform.position;
		time.text = p.pointA.x.ToString () +" "+ p.pointA.xunit;
		ATitle.text = p.pointA.series.name;
		A.text = p.pointA.y.ToString () + " " + p.pointA.yunit;
		BTitle.text = p.pointB.series.name;
		B.text = p.pointB.y.ToString () + " " + p.pointB.yunit;
		Show ();
	}

}
