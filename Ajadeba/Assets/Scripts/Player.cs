using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public string myName;
	public Sprite flag;

	bool active = false;
	bool alive = true;


	void Start () 
	{
		SetFlag ();
	}

	void SetFlag()
	{
		GameObject flagContainer=GameObject.Find ("Flag Collection/Flag1");
		SpriteRenderer fSR = flagContainer.GetComponent<SpriteRenderer> ();
		flag = fSR.sprite;
	}

	public void SetActive (bool act)
	{
		active = act;
	}

	public bool IsActive ()
	{
		return active;
	}

	public bool IsAlive()
	{
		return alive;
	}

}
