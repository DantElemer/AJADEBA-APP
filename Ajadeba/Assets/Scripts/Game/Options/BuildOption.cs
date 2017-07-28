using UnityEngine;
using System.Collections;

public class BuildOption : MonoBehaviour {

	public GameObject cantBuildPref;
	protected float scale = 1;

	void Awake ()
	{
		if (!CanBuild (MapHandler.instance.chosenField))
			Disable ();
	}

	protected void Disable()
	{
		GameObject darkening = Instantiate (cantBuildPref);
		darkening.transform.SetParent (gameObject.transform);
		darkening.transform.position = gameObject.transform.position;
		darkening.transform.localScale = new Vector3 (scale, scale, 1);

		gameObject.GetComponent<BoxCollider2D> ().enabled = false;
	}

	protected virtual bool CanBuild (Field where)
	{
		return where.IsBlankForBuilding ();
	}
}
