using UnityEngine;
using System.Collections;

public class BuildOption : MonoBehaviour {

	public GameObject cantBuildPref;

	void Awake ()
	{
		if (!CanBuild (MapHandler.instance.chosenField))
			Disable ();
	}

	protected void Disable() //TODO roads must override for scaling
	{
		GameObject darkening = Instantiate (cantBuildPref);
		darkening.transform.SetParent (gameObject.transform);
		darkening.transform.position = gameObject.transform.position;

		gameObject.GetComponent<BoxCollider2D> ().enabled = false;
	}

	protected bool CanBuild (Field where) //TODO roads must override
	{
		return where.isBlankForBuilding ();
	}
	// TODO: 
}
