using UnityEngine;
using System.Collections;

//play current entities sound
public class playSingleSound : MonoBehaviour {

	private bool found;
	private int languageChoice;
	
	// Use this for initialization
	void Start () {
		found = false;
	}
	
	void OnCollisionEnter2D (Collision2D col){
		if (found == false) {
			found = true;
			AudioSource[] aSource = GameObject.Find(this.name).GetComponents<AudioSource>();
			aSource[0].Play();
		}
	}
}
