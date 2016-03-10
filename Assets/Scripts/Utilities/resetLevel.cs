using UnityEngine;
using System.Collections;

public class resetLevel : MonoBehaviour {

	void OnMouseDown() {
		Application.LoadLevelAsync (Application.loadedLevel);
	}
}
