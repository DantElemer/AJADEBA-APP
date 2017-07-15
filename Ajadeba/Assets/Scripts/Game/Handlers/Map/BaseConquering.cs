using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseConquering : MonoBehaviour {

	public void ConquerCheck()
	{
		bool newStrongCreated = false; 
		foreach (Field[] row in MapHandler.instance.fields)
			foreach (Field f in row)
				if (f != null) //could use inMap as well
				if (f.HasPart (Field.STRONGHOLD_BASE)) {
					int[] strengths = new int[PlayerHandler.instance.players.Length];
					int i = 0;
					foreach (Player p in PlayerHandler.instance.players) { //yeah using foreach and use a counter...
						strengths [i] = f.myStrongholdBase.strength (p);
						i++;
					}
					List<int> maxes = new List<int>();
					int max = -1; //assuming there is no negative strength...
					for (int j = 0; j < strengths.Length; j++) { //finding the max strengths
						if (strengths [j] > max) {
							max = strengths [j];
							maxes.Clear ();
							maxes.Add (j);
						} else if (strengths [j] == max) {
							maxes.Add (j);
						}
					}
					if (max > 0) { //valakié mostmár lesz
						if (maxes.Count == 1) //the normal case...
							Debug.Log("a legtöbbet küldőé");//TODO kapja meg
						else
							Debug.Log("többen küldenek legtöbb katonát, további munka kell :(");//TODO
					}
					//TODO...
				}
		// végigmenni az összes mezőn, megvizsgálni a base-ket, hogy
		// van-e játékos aki megkaphatja
		// ha igen
		// ha egy, az megkapja *
		// ha több, melyik küld a legtöbbet,
		// ha egyenlőség, melyik építette,
		// ha egyik se az aktuális kapja *
		// * a Base-t el kell távolítani, Strong-ot adni
		// beállítani az owner-ét
		// Inic (ezzel kap területet)
		// tesztelni, hogy vannak-e lemészárolandó dolgok (lemészárolni őket)
		// újra, ha lett új strong (a lánc lehetőség miatt)
	}
}
