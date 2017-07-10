using UnityEngine;
using System.Collections;

public class Field : MonoBehaviour {

	public int index = -1; 
	public const float WIDTH = 0.3f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{
		MapHandler.FieldPressed (index);
		//Debug.Log("Hi");
	}
}
