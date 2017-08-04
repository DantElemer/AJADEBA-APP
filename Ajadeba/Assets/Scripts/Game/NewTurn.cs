using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewTurn : MonoBehaviour {

	public Text txtNewTurn;

	// Use this for initialization
	void Start () 
	{
		MapHandler.instance.SetStatus(MapHandler.FROZEN_STATE);
		txtNewTurn.text=PlayerHandler.instance.currentPlayer.myName+" köre!";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ResumeGame()
	{
		MapHandler.instance.SetStatus(MapHandler.NORMAL_STATE);
		SceneManager.UnloadScene ("NewTurn");
	}
}
