using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

//	void Start () 
//	{
//	
//	}
//
//	void Update ()
//	{
//	
//	}

	public void Tutorial()
	{
		SceneManager.LoadScene("Tutorial");
	}

	public void Play() //starts settings 
	{
		SceneManager.LoadScene("Settings");
	}

	public void Exit()
	{
		Application.Quit ();
	}
}
