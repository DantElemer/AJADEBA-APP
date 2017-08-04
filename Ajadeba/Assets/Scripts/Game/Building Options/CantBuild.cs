using UnityEngine;
using System.Collections;

public class CantBuild : MonoBehaviour {

	void OnMouseOver()
	{
		if (Input.GetMouseButtonUp (0))
			Debug.Log ("You cannot build that one here!");
	}
}
