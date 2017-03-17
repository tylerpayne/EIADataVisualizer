using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOOKORIGIN : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (Vector3.zero);
		transform.Rotate (Vector3.up * 180);
	}
}
