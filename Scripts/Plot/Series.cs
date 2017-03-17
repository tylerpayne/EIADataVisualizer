using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Series {

	public List<Point> data;
	public string name;
	public string description;
	public string series_id;
	public JSONNode rawJson;
	public float ymin;
	public float ymax;
	public float xmin;
	public float xmax;
	public Series()
	{
		data = new List<Point> ();
	}

}
