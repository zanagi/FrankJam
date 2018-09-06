using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableBuilding : SelectableObject {

	public string buildingName;
	[TextArea]
	public string description;

	// Use this for initialization
	protected override void Start () {
		base.Start();

		windowInstance.title.text = buildingName.ToUpper();
		windowInstance.price.gameObject.SetActive(false);
		windowInstance.description.transform.localPosition
			= windowInstance.price.transform.localPosition;
		windowInstance.description.text = description.ToUpper();
		windowInstance.actionButton1.gameObject.SetActive(false);
	}
}
