using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;

public class Browser : MonoBehaviour {
	public static Browser instance;
	public string API_KEY;
	public string ROOT_CATEGORY_KEY;
	public GameObject ListItemType;
	public ScrollArea scrollArea;
	public Text title;

	public Color categoryColor;
	public Color seriesColor;

	private string category_url = "http://api.eia.gov/category/?api_key=9094bdae34fd5f528c4c1c2be5286186&category_id=";
	private JSONNode thispage;

	private List<string> history;

	private List<GameObject> currentItems;

	public Vector3 initialPosition;
	public float initialOffset;

	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
		} else
		{
			Debug.Log ("Too many browsers!");
		}
		thispage = null;
		currentItems = null;
		history = new List<string> ();
		currentItems = new List<GameObject> ();
		GoToCategory (ROOT_CATEGORY_KEY);

	}
	
	// Update is called once per frame
	void Update () {
		if (thispage != null && currentItems.Count == 0) {
			Populate ();
		}
	}

	public void GoBack()
	{
		history.RemoveAt (history.Count - 1);
		string id = history [history.Count - 1];
		history.RemoveAt (history.Count - 1);
		GoToCategory (id);
	}

	public void GoToCategory(string id)
	{
		Debug.Log ("LOADING!");
		thispage = null;
		for (int i = 0; i < scrollArea.content.childCount; i++) {
			Destroy (scrollArea.content.GetChild (i).gameObject);
		}
		currentItems.Clear ();
		scrollArea.content_offset = initialOffset;
		scrollArea.content.localPosition = initialPosition;
		StartCoroutine ("DownloadCategory",id);
	}

	private IEnumerator DownloadCategory(string id)
	{
		WWW eia = new WWW (category_url + id);
		yield return eia;
		history.Add (id);
		thispage = JSON.Parse(eia.text);
		title.text = thispage ["category"] ["name"];
	}

	private void Populate()
	{
		JSONArray categories = thispage ["category"] ["childcategories"].AsArray;
		foreach (JSONNode n in categories)
		{
			GameObject ln = Instantiate (ListItemType);
			ListItem li = ln.GetComponent<ListItem> ();
			li.category_id = n ["category_id"];
			li.name = n ["name"];
			li.text.text = li.name;
			li.GetComponent<Image> ().color = categoryColor;

			ln.transform.position = new Vector3 (0, 0, 0);
			ln.transform.SetParent (scrollArea.content);

			Vector3 lpos = ln.transform.localPosition;
			lpos.y = scrollArea.content_offset;
			lpos.x = 0f;
			lpos.z = 0f;
			ln.transform.localScale = new Vector3 (1, 1, 1);
			ln.transform.localRotation = Quaternion.identity;
			ln.transform.localPosition = lpos;
			scrollArea.content_offset -= scrollArea.content_pad;

			ln.SetActive (true);
			currentItems.Add (ln);
		}

		JSONArray series = thispage ["category"] ["childseries"].AsArray;
		foreach (JSONNode n in series)
		{
			GameObject ln = Instantiate (ListItemType);
			ln.AddComponent<DragDrop> ();
			ln.GetComponent<DragDrop> ().parentOverride = transform;
			ListItem li = ln.GetComponent<ListItem> ();
			li.series_id = n ["series_id"];
			li.category_id = null;
			li.name = n ["name"];
			li.text.text = li.name;
			li.GetComponent<Image> ().color = seriesColor;

			ln.transform.position = new Vector3 (0, 0, 0);
			ln.transform.SetParent (scrollArea.content);

			Vector3 lpos = ln.transform.localPosition;
			lpos.y = scrollArea.content_offset;
			lpos.x = 0f;
			lpos.z = 0f;
			ln.transform.localScale = new Vector3 (1, 1, 1);
			ln.transform.localRotation = Quaternion.identity;
			ln.transform.localPosition = lpos;
			scrollArea.content_offset -= scrollArea.content_pad;

			ln.SetActive (true);
			currentItems.Add (ln);
		}
	}



}
