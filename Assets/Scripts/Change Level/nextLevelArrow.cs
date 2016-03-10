using UnityEngine;
using System.Collections;

public class nextLevelArrow : MonoBehaviour {
	
	void OnMouseDown() {
		if (Application.loadedLevel == 3) {
			Application.LoadLevelAsync (1);
		} else {
			if(Application.loadedLevel == Application.levelCount -1){
				Application.LoadLevelAsync(0);
			} else{
				Application.LoadLevelAsync (Application.loadedLevel + 1);
			}
		}
	}
}
