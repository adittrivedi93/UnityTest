using UnityEngine;
using System.Collections;

public class switchCharacters : MonoBehaviour {

	private counterToSwitchCharacters characterSwitch;
	private ArrayList elephants, frogs;
	public static string animalName;

	void Start() {
		animalName = "elephant";
		characterSwitch = new counterToSwitchCharacters ();
		elephants = getGameObjects ("elephant");
		frogs = getGameObjects ("frog");
		for (int i = 0; i< frogs.Count; i++) {
			GameObject.Find (frogs[i].ToString()).GetComponent<SpriteRenderer> ().color 
				= new Color (1f, 1f, 1f, 0f);
		}
	}

	void OnMouseDown() {
		characterSwitch.incrementCounter ();
		animalName = characterSwitch.getAnimal ();
		if (characterSwitch.getCounter () == 1) {
			for (int i = 0; i< elephants.Count; i++) {
				GameObject.Find (elephants [i].ToString ()).GetComponent<SpriteRenderer> ().color 
					= new Color (1f, 1f, 1f, 0f);
				GameObject.Find (frogs [i].ToString ()).GetComponent<SpriteRenderer> ().color 
					= new Color (1f, 1f, 1f, 1f);
			}
		} else {
			for (int i = 0; i< elephants.Count; i++) {
				GameObject.Find (elephants [i].ToString ()).GetComponent<SpriteRenderer> ().color 
					= new Color (1f, 1f, 1f, 1f);
				GameObject.Find (frogs [i].ToString ()).GetComponent<SpriteRenderer> ().color 
					= new Color (1f, 1f, 1f, 0f);
			}
		}
	}

	private ArrayList getGameObjects(string gameObject){
		ArrayList items = new ArrayList ();
		GameObject[] gameObjects = (GameObject[])Object.FindObjectsOfType (typeof(GameObject));
		for (int i=0; i<gameObjects.Length; i++) {
			if (gameObjects [i].name.Contains (gameObject)) {
				items.Add (gameObjects [i].name);
			}
		}
		return items;
	}
}
