using UnityEngine;
using System.Collections;

public class saveCubeWithTargetName {

	private string cubeName;
	private string targetName;
	private Sprite objectSprite;

	public saveCubeWithTargetName(string cubeName, string targetName, Sprite objectSprite) {
		this.cubeName = cubeName;
		this.targetName = targetName;
		this.objectSprite = objectSprite;
	}

	public Sprite getObjectSprite(){
		return this.objectSprite;
	}

	public string getCubeName(){
		return this.cubeName;
	}

	public string getTargetName (){
		return this.targetName;
	}

	public void saveCubeName(string cubeName){
		this.cubeName = cubeName;
	}

	public void saveTargetName(string targetName){
		this.targetName = targetName;
	}

	public void saveTargetName(Sprite objectSprite){
		this.objectSprite = objectSprite;
	}

}
