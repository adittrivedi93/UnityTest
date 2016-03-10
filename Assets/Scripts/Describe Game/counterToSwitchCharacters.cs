using UnityEngine;
using System.Collections;

public class counterToSwitchCharacters {

	private int characterCounter = 0;
	private string animal;
	
	public void incrementCounter(){
		if (characterCounter == 0) {
			characterCounter++;
			animal = "frog";
		} else {
			characterCounter--;
			animal = "elephant";
		}
	}

	public int getCounter(){
		return this.characterCounter;
	}

	public string getAnimal(){
		return this.animal;
	}
}
