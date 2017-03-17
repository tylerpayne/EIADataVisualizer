using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler {

	public enum DragDropMode
	{
		Drag,
		Drop
	}

	public DragDropMode mode = DragDropMode.Drag;
	public float speed = 500f;
	public Transform initialParent;
	public Transform parentOverride;

	private bool dragging = false;
	private PointerEventData lastEventData;
	private Vector3 initialPosition;

	// Use this for initialization
	void Start () {
		initialPosition = transform.localPosition;
		initialParent = transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
		if (dragging) {
			Vector3 r = transform.right;
			Vector3 u = transform.up;
			Vector3 pos = transform.position;
			pos += speed*lastEventData.delta.x * -r;
			pos += speed*lastEventData.delta.y * u;
			transform.position = pos;
			transform.LookAt (GameObject.FindGameObjectWithTag ("MainCamera").transform.position);
			transform.Rotate (0, 180, 0);

		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (mode == DragDropMode.Drag) {
			transform.SetParent (parentOverride.transform);
			lastEventData = eventData;
			dragging = true;
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (mode == DragDropMode.Drag)
		{
			PointerEventData ed = new PointerEventData (EventSystem.current);
			List<RaycastResult> rr = new List<RaycastResult> ();
			EventSystem.current.RaycastAll (ed, rr);

			if (rr.Count > 0) {
				for (int i = 0; i < rr.Count; i++) {
					GameObject curr = rr [i].gameObject;
					DragDrop dd = curr.GetComponent<DragDrop> ();
					if (dd != null) {
						if (dd.mode == DragDropMode.Drop) {
							ListItem myLI = GetComponent<ListItem> ();
							ListItem currLI = curr.GetComponent<ListItem> ();
							currLI.series_id = myLI.series_id;
							currLI.name = myLI.name;
							currLI.text.text = myLI.name;
						}
					}
				}
			}
			transform.position = Vector3.zero;
			transform.SetParent (initialParent);
			transform.localPosition = initialPosition;
			dragging = false;
			lastEventData = null;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		
	}	

}
