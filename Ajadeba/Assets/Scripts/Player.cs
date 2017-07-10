using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public string myName;

	bool active = false;
	bool alive = true;


	void Start () 
	{
	
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
