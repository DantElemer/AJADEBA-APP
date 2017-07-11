using UnityEngine;
using System.Collections;

public class BarrackOption : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		GameObject flag = gameObject.transform.Find ("Flag").gameObject;
		SpriteRenderer flagSR = flag.GetComponent<SpriteRenderer> ();

		GameObject flagContainer=GameObject.Find ("Flag Collection/Flag1");
		SpriteRenderer fSR = flagContainer.GetComponent<SpriteRenderer> ();
		flagSR.sprite= fSR.sprite/*TODO current player .flag*/;
	}
	
	void OnMouseOver()
	{
		Debug.Log ("over");
		if (Input.GetMouseButtonUp (0)) {
			MapHandler.instance.fields [MapHandler.instance.chosenField].addBarrack (/*TODO current player*/);
			Debug.Log ("chosen");
		}
	}
}
