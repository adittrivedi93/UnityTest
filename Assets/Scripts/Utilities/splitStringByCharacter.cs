using UnityEngine;
using System.Collections;

public class splitStringByCharacter{
	
	public string[] getStrings(string item){
		return item.Split(new string[] { "_" }, System.StringSplitOptions.None);
	}

	public string[] getStrings(string item, string character){
		return item.Split(new string[] { character }, System.StringSplitOptions.None);
	}
		
}
