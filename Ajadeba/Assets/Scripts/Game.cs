using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class DomiTime : MonoBehaviour
{
	public int seconds;
	public int minutes;
	public bool writeZeros; //like: 0 minute 12 seconds

	string Time()
	{
		string ret="";
		if (minutes > 0)
			if (minutes == 1)
				ret += "1 minute";
			else
				ret += minutes.ToString () + "minutes";
		else if (writeZeros)
			ret += "0 minute";
		if (seconds > 0)
			if (seconds == 1)
				ret += "1 second";
			else
				ret += seconds.ToString () + "seconds";
		else if (writeZeros)
			ret += "0 second";
		return ret;
	}

	public static DomiTime MakeDomiTime (string time)
	{
		return null;//TODO
	}

	public static DomiTime MakeDomiTime (int seconds, int minutes=0)
	{
		return null;//TODO
	}

	//TODO operators
}



public class Game : MonoBehaviour {


	public Text timeLeft;

	// Use this for initialization
	void Start () 
	{
		timeLeft.text = "Time left: " + PlayerPrefs.GetString ("Turn time"); //TODO use DomiTime
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
}
