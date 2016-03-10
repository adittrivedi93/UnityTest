using UnityEngine;
using System.Collections;

public class Utilities {

	public ArrayList getSpriteGameObjects(){
		ArrayList items = new ArrayList ();
		GameObject[] gameObjects = (GameObject[]) Object.FindObjectsOfType(typeof(GameObject));
		for(int i=0; i<gameObjects.Length; i++){
			if(gameObjects[i].GetComponent<SpriteRenderer>() != null){
				items.Add(gameObjects[i].name);
			}
		}
		return items;
	}

	public ArrayList getGameObjects(string gameObject){
		ArrayList items = new ArrayList ();
		GameObject[] gameObjects = (GameObject[]) Object.FindObjectsOfType(typeof(GameObject));
		for(int i=0; i<gameObjects.Length; i++){
			if(gameObjects[i].name.Substring(0,gameObject.Length).Contains(gameObject)){
				items.Add(gameObjects[i].name);
			}
		}
		return items;
	}
}
