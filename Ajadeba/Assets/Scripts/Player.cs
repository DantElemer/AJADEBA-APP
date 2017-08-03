using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public string myName;
	public string nation;
	public Sprite flag;

	bool active = false;
	bool alive = true;

	List<Character> myChars = new List<Character> (); //watch out, you need casting for specified stuff!


	public void SetFlag()
	{
		flag = Resources.Load<Sprite> ("Nations/"+nation+"/Flag");
		if (flag == null)
			Debug.Log ("gebasz a zászló betöltésénél");
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

	public bool HasChar(string character)
	{
		foreach (Character c in myChars)
			if (c.name == character)
				return true;
		return false;
	}

	public void AddCharacter(Character newCh)
	{
		myChars.Add(newCh);
		Debug.Log (newCh.name + " added to " + myName);
		/*Mason m = newCh as Mason;
		if (m != null)
			m.Activated ();*/
	}

}
