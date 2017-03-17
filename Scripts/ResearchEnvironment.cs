using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchEnvironment : MonoBehaviour {
	public static ResearchEnvironment instance;

	public GameObject plotType;

	public List<Plot> plots;

	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
		} else {
			Debug.Log ("Attempting to make too many ResearchEnvironment Instances!");
		}
		plotType.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
