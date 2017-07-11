using UnityEngine;
using System.Collections;

public class Barrack : MonoBehaviour {

	public Player owner;

	public void inic()
	{
		//setting flag
		GameObject flag = gameObject.transform.Find ("Flag").gameObject;
		SpriteRenderer flagSR = flag.GetComponent<SpriteRenderer> ();
		flagSR.sprite = owner.flag;
	}
}
