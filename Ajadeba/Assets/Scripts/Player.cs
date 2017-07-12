using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public string myName;
	public string nation;
	public Sprite flag;

	bool active = false;
	bool alive = true;


	void Start () 
	{
		SetFlag ();
	}

	void SetFlag()
	{
		flag = Resources.Load<Sprite> ("Nations/"+nation+"/Flag");
		if (flag == null)
			Debug.Log ("gebasz a betöltésnél");
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
