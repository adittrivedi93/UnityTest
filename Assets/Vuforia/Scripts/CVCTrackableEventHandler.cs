/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Qualcomm Connected Experiences, Inc.
==============================================================================*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Vuforia
{
	/// <summary>
	/// A custom handler that implements the ITrackableEventHandler interface.
	/// </summary>
	public class CVCTrackableEventHandler : MonoBehaviour,
	ITrackableEventHandler
	{
		#region PRIVATE_MEMBER_VARIABLES
		
		private TrackableBehaviour mTrackableBehaviour;

		private string preCubeyOne, preCubeyTwo, firstTarget, secondTarget;

		private List<saveCubeWithTargetName> customCube;

		private int firstNumber, secondNumber;

		private bool found;

		private saveGameState gameState;

		//Cube switches with this item
		public string objectSwitchNameOne, objectSwitchNameTwo;
		
		#endregion // PRIVATE_MEMBER_VARIABLES
		
		#region UNTIY_MONOBEHAVIOUR_METHODS

		void Start()
		{
			found = false; 
			gameState = new saveGameState ();
			gameState.setAnimating (false);

			firstNumber = 0;
			secondNumber = 0;

			while (firstNumber == secondNumber) {
				firstNumber = Random.Range (0, 4);
				secondNumber = Random.Range (0, 4);
			}

			mTrackableBehaviour = GetComponent<TrackableBehaviour>();
			if (mTrackableBehaviour)
			{
				mTrackableBehaviour.RegisterTrackableEventHandler(this);
			}


			customCube = new List<saveCubeWithTargetName>();
			customCube.Add(new saveCubeWithTargetName("cubey_cyan","FrameMarker0", GameObject.Find("cubey_cyan").GetComponent<SpriteRenderer>().sprite));
			customCube.Add(new saveCubeWithTargetName("cubey_green","FrameMarker3",GameObject.Find("cubey_green").GetComponent<SpriteRenderer>().sprite));
			customCube.Add(new saveCubeWithTargetName("cubey_magenta","FrameMarker1",GameObject.Find("cubey_magenta").GetComponent<SpriteRenderer>().sprite));
			customCube.Add(new saveCubeWithTargetName("cubey_yellow","FrameMarker2",GameObject.Find("cubey_yellow").GetComponent<SpriteRenderer>().sprite));

			GameObject.Find("cubey_one").GetComponent<SpriteRenderer>().sprite = customCube[firstNumber].getObjectSprite();
			GameObject.Find("cubey_two").GetComponent<SpriteRenderer>().sprite = customCube[secondNumber].getObjectSprite();

			GameObject.Find ("cubey_one").GetComponent<Transform> ().localPosition = new Vector3 (-2.86F, -0.45F, 0F);
			GameObject.Find ("cubey_two").GetComponent<Transform> ().localPosition = new Vector3 (2.86F, -0.45F, 0F);
		}
		
		private ArrayList getGameObjects(string gameObject){
			ArrayList items = new ArrayList ();
			GameObject[] gameObjects = (GameObject[]) Object.FindObjectsOfType(typeof(GameObject));
			for(int i=0; i<gameObjects.Length; i++){
				if(gameObjects[i].name.Substring(0,gameObject.Length).Contains(gameObject)){
					items.Add(gameObjects[i].name);
				}
			}
			return items;
		}
		
		#endregion // UNTIY_MONOBEHAVIOUR_METHODS
		
		
		
		#region PUBLIC_METHODS
		
		/// <summary>
		/// Implementation of the ITrackableEventHandler function called when the
		/// tracking state changes.
		/// </summary>
		public void OnTrackableStateChanged(
			TrackableBehaviour.Status previousStatus,
			TrackableBehaviour.Status newStatus)
		{
			if (newStatus == TrackableBehaviour.Status.DETECTED ||
			    newStatus == TrackableBehaviour.Status.TRACKED ||
			    newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
			{
				OnTrackingFound();
			}
			else
			{
				OnTrackingLost();
			}
		}
		
		#endregion // PUBLIC_METHODS
		
		
		
		#region PRIVATE_METHODS
		
		
		private void OnTrackingFound()
		{
			found = true;

			firstTarget = "";
			secondTarget = "";
			for (int i = 0; i<customCube.Count; i++) {
				if(GameObject.Find("cubey_one").GetComponent<SpriteRenderer>().sprite.name == customCube[i].getCubeName()){
					firstTarget = customCube[i].getTargetName();
					preCubeyOne = customCube[i].getCubeName();
				} else if(GameObject.Find("cubey_two").GetComponent<SpriteRenderer>().sprite.name == customCube[i].getCubeName()){
					secondTarget = customCube[i].getTargetName();
					preCubeyTwo = customCube[i].getCubeName();
				}
			}
			Debug.Log ("mTrackable: " + mTrackableBehaviour.TrackableName);
			Debug.Log ("First target: " + firstTarget);

			if (mTrackableBehaviour.TrackableName == firstTarget) {
				GameObject.Find("cubey_one").GetComponent<SpriteRenderer>().sprite = GameObject.Find(objectSwitchNameOne).GetComponent<SpriteRenderer>().sprite;
				GameObject.Find("cubey_one").GetComponent<Transform>().transform.localScale = new Vector3(0.25F, 0.25F, 1F);
				GameObject.Find("cubey_one").GetComponent<AudioSource>().Play();
			}

			if (mTrackableBehaviour.TrackableName == secondTarget) {
				GameObject.Find("cubey_two").GetComponent<SpriteRenderer>().sprite = GameObject.Find(objectSwitchNameTwo).GetComponent<SpriteRenderer>().sprite;
				GameObject.Find("cubey_two").GetComponent<Transform>().transform.localScale = new Vector3(0.25F, 0.25F, 1F);
				GameObject.Find("cubey_two").GetComponent<AudioSource>().Play();
			}

			if (findTotalCubes () == 2) {
				gameState.setAnimating (true);
				float audioLengthOne = GameObject.Find("cubey_one").GetComponent<AudioSource>().clip.length;
				float audioLengthTwo = GameObject.Find("cubey_two").GetComponent<AudioSource>().clip.length;
				StartCoroutine(animateCVC(audioLengthOne, audioLengthTwo));
			} 
		}

		IEnumerator animateCVC(float audioLengthOne, float audioLengthTwo){
			yield return new WaitForSeconds(audioLengthOne);
			GameObject.Find ("cubey_one").transform.position += 
				GameObject.Find ("cubey_one").transform.up * 100.0f * Time.smoothDeltaTime;
			GameObject.Find("cubey_one").GetComponent<AudioSource>().Play();

			yield return new WaitForSeconds(audioLengthOne);
			GameObject.Find ("cubey_one").GetComponent<SpriteRenderer> ().color 
				= new Color (1f, 1f, 1f, 0.5F);

			GameObject.Find ("cubey_two").transform.position += 
				GameObject.Find ("cubey_two").transform.up * 100.0f * Time.smoothDeltaTime;
			GameObject.Find("cubey_two").GetComponent<AudioSource>().Play();
			yield return new WaitForSeconds(audioLengthTwo);
			GameObject.Find ("cubey_two").GetComponent<SpriteRenderer> ().color 
				= new Color (1f, 1f, 1f, 0F);
			GameObject.Find ("cubey_one").GetComponent<SpriteRenderer> ().color 
				= new Color (1f, 1f, 1f, 0F);

			GameObject.Find ("cubey_one").GetComponent<Transform> ().position = new Vector3 (-2.86F, -0.45F, 0F);
			GameObject.Find ("cubey_two").GetComponent<Transform> ().position = new Vector3 (2.86F, -0.45F, 0F);

			GameObject.Find ("object_cvc").GetComponent<SpriteRenderer> ().color 
				= new Color (1f, 1f, 1f, 1F);
			GameObject.Find("object_cvc").GetComponent<AudioSource>().Play();
			float audioLengthObject = GameObject.Find("object_cvc").GetComponent<AudioSource>().clip.length;

			gameState.setAnimating (false);

			yield return new WaitForSeconds(audioLengthObject);
			GameObject.Find ("right_arrow").GetComponent<SpriteRenderer> ().color 
				= new Color (1f, 1f, 1f, 1F);
		}

		private int findTotalCubes(){
			int total = 0;
			if (!(GameObject.Find ("cubey_one").GetComponent<SpriteRenderer> ().sprite.name.Contains("cubey"))) {
				total++;
			}
			if (!(GameObject.Find ("cubey_two").GetComponent<SpriteRenderer> ().sprite.name.Contains("cubey"))) {
				total++;
			}
			return total;
		}
		
		
		private void OnTrackingLost()
		{
			if (found == true) {
				Debug.Log("Still animating: " + gameState.getAnimating());
				if(gameState.getAnimating() == false){
					GameObject.Find ("object_cvc").GetComponent<SpriteRenderer> ().color 
						= new Color (1f, 1f, 1f, 0F);

					if (mTrackableBehaviour.TrackableName == firstTarget) {
						GameObject.Find ("cubey_one").GetComponent<SpriteRenderer> ().sprite = GameObject.Find (preCubeyOne).GetComponent<SpriteRenderer> ().sprite;
						GameObject.Find ("cubey_one").GetComponent<Transform> ().transform.localScale = new Vector3 (2F, 2F, 2F);
						GameObject.Find ("cubey_one").GetComponent<SpriteRenderer> ().color 
							= new Color (1f, 1f, 1f, 1F);
						GameObject.Find ("cubey_two").GetComponent<SpriteRenderer> ().color 
							= new Color (1f, 1f, 1f, 1F);
					}

					if (mTrackableBehaviour.TrackableName == secondTarget) {
						GameObject.Find ("cubey_two").GetComponent<SpriteRenderer> ().sprite = GameObject.Find (preCubeyTwo).GetComponent<SpriteRenderer> ().sprite;
						GameObject.Find ("cubey_two").GetComponent<Transform> ().transform.localScale = new Vector3 (2F, 2F, 2F);
						GameObject.Find ("cubey_two").GetComponent<SpriteRenderer> ().color 
							= new Color (1f, 1f, 1f, 1F);
						GameObject.Find ("cubey_one").GetComponent<SpriteRenderer> ().color 
							= new Color (1f, 1f, 1f, 1F);
					}
				}
			}

			Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
		}
		#endregion // PRIVATE_METHODS
	}
}