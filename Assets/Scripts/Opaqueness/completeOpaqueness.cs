using UnityEngine;
using System.Collections;

//Turns object completely transparent
public class completeOpaqueness : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		GameObject.Find (this.name).GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0f);
	}
}
