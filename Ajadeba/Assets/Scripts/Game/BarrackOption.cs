using UnityEngine;
using System.Collections;

public class BarrackOption : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		GameObject flag = gameObject.transform.Find ("Flag");
		SpriteRenderer flagSR = flag.GetComponent<SpriteRenderer> ();
		//flagSR.sprite= /*TODO current player .flag*/;
	}
	
	void OnMouseOver()
	{
		//if (Input.GetMouseButtonDown(0))
		//	MapHandler.instance.fields [MapHandler.instance.chosenField].addBarrack (/*TODO current player*/);
	}
}
