using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public string myName;
	public string nation;
	public Sprite flag;

	bool active = false;
	bool alive = true;

	//public List<Character> myChars = new List<Character> (); //it's only public to see it in the editor!


	/*void Start () 
	{
		SetFlag ();
	}*/

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
		return false; //TODO ...
	}

	public void AddCharacter(string newCh)
	{
		if (newCh == Character.Mason)
			gameObject.AddComponent<Mason> ();
		else
			Debug.Log ("Nincs ilyen karakter!");
	}

}
