using UnityEngine;
using System.Collections;

public class RoadOption : MonoBehaviour {

	public GameObject northRoadOpPref;
	GameObject nRO;
	public GameObject eastRoadOpPref;
	GameObject eRO;
	public GameObject southRoadOpPref;
	GameObject sRO;
	public GameObject westRoadOpPref;
	GameObject wRO;

	bool open=false;


	void ShowRoadOps()
	{
		nRO = Instantiate (northRoadOpPref);
		nRO.transform.position = gameObject.transform.position + Vector3.up / 2;
		eRO = Instantiate (eastRoadOpPref);
		eRO.transform.position = gameObject.transform.position + Vector3.right / 2;
		sRO = Instantiate (southRoadOpPref);
		sRO.transform.position = gameObject.transform.position + Vector3.down / 2;
		wRO = Instantiate (westRoadOpPref);
		wRO.transform.position = gameObject.transform.position + Vector3.left / 2;

		open = true;
	}

	void OnMouseEnter()
	{
		if (!open)
			ShowRoadOps ();
	}

	public void CloseRoadOps()
	{
		if (open) 
		{
			Destroy (nRO.gameObject);
			Destroy (eRO.gameObject);
			Destroy (sRO.gameObject);
			Destroy (wRO.gameObject);
		}	
		open = false;
	}
}
