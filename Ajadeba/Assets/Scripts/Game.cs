using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Game : MonoBehaviour {


	public Text timeLeft;

	// Use this for initialization
	void Start () 
	{
		timeLeft.text = "Time left: " + PlayerPrefs.GetString ("Turn time");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
