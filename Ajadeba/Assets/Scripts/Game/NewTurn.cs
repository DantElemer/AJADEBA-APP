using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//TODO CharacterPanelen lehet dolgozni miközben ez meg van nyitva
public class NewTurn : MonoBehaviour {

	public Text txtNewTurn;

	void Start () 
	{
		MapHandler.instance.status=MapHandler.FROZEN_STATE;
		txtNewTurn.text=PlayerHandler.instance.currentPlayer.myName+" köre!";
	}

	public void ResumeGame()
	{
		MapHandler.instance.status=MapHandler.NORMAL_STATE;
		SceneManager.UnloadScene ("NewTurn");
	}
}
