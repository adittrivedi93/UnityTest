using UnityEngine;
using System.Collections;


//This script makes a character jump based on the object it has collided with
public class characterJumping : MonoBehaviour {

	private bool found;

	// Use this for initialization
	void Start () {
		found = false;
	}
	
	// Update is called once per frame
	void OnCollisionEnter2D (Collision2D col){
		if (found == false) {
			GameObject.Find (this.name).transform.position += 
				GameObject.Find (this.name).transform.up * 100.0f * Time.smoothDeltaTime;
			found = true;
		}
	}
}
