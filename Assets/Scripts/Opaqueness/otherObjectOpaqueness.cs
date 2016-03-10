using UnityEngine;
using System.Collections;

// Turn all other objects starting with key word 'object' to go opaque
public class otherObjectOpaqueness : MonoBehaviour {

	private bool found;
	private ArrayList gObject;
	private splitStringByCharacter stringSplitter;

	// Use this for initialization
	void Start () {
		found = false;
		gObject = new ArrayList ();
		stringSplitter = new splitStringByCharacter ();
		GameObject[] gameObjects = (GameObject[]) Object.FindObjectsOfType(typeof(GameObject));
		string [] objectSplit = stringSplitter.getStrings(this.name);
		for(int i =0; i< gameObjects.Length; i++){
			if(!(gameObjects[i].name.Contains(objectSplit[objectSplit.Length-1]))){
				gObject.Add(gameObjects[i].name);
			}
		}
	}
	
	void OnCollisionEnter2D (Collision2D col){
		if (found == false) {
			found = true;
			Debug.Log("Length of list: " +gObject.Count);
			for(int i =0; i<gObject.Count; i++){
				if(!(GameObject.Find(gObject[i].ToString()).GetComponent<SpriteRenderer>().Equals(null))){
					GameObject.Find (gObject[i].ToString()).GetComponent<SpriteRenderer> ().color 
						= new Color (1f, 1f, 1f, 0.5f);
				}
			}
		}
	}
}
