using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewTurn : MonoBehaviour {

	public Text txtNewTurn;

	// Use this for initialization
	void Start () 
	{
		txtNewTurn.text=PlayerHandler.players[PlayerHandler.currPlayer].myName+" köre!";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ResumeGame()
	{
		SceneManager.UnloadScene ("NewTurn");
	}
}
