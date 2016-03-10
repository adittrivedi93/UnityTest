using UnityEngine;
using System.Collections;

public class constantJumping : MonoBehaviour {

	void OnCollisionEnter2D (Collision2D col){
		GameObject.Find (this.name).transform.position += 
			GameObject.Find (this.name).transform.up * 100.0f * Time.smoothDeltaTime;
	}
}
