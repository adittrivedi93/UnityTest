using UnityEngine;
using System.Collections;

public class changeLevelMainMenu : MonoBehaviour {

	private bool found;
	public int level;
	
	// Use this for initialization
	void Start () {
		found = false;
	}
	
	void OnCollisionEnter2D (Collision2D col){
		if (found == false) {
			found = true;
			float audioLength = GameObject.Find(this.name).GetComponent<AudioSource>().clip.length;
			StartCoroutine (changeLevel(audioLength));
		}
	}

	void OnMouseDown() {
		Application.LoadLevelAsync (0);
	}

	IEnumerator changeLevel(float audioLength){
		yield return new WaitForSeconds(audioLength + 0.5F);
		Application.LoadLevelAsync (level);
	}
}
