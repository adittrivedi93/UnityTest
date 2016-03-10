/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Qualcomm Connected Experiences, Inc.
==============================================================================*/

using UnityEngine;
using System.Collections;

namespace Vuforia
{
	/// <summary>
	/// A custom handler that implements the ITrackableEventHandler interface.
	/// </summary>
	public class describeTrackableEventHandler : MonoBehaviour,
	ITrackableEventHandler
	{
		#region PRIVATE_MEMBER_VARIABLES
		
		private TrackableBehaviour mTrackableBehaviour;
		
		private bool found = false;
		
		#endregion // PRIVATE_MEMBER_VARIABLES
		
		#region UNTIY_MONOBEHAVIOUR_METHODS
		
		void Start()
		{
			GameObject.Find ("elephant_magenta").GetComponent<Animator> ().enabled = false; 
			GameObject.Find ("elephant_cyan").GetComponent<Animator> ().enabled = false; 
			GameObject.Find ("elephant_green").GetComponent<Animator> ().enabled = false; 

			GameObject.Find ("frog_magenta").GetComponent<Animator> ().enabled = false; 
			GameObject.Find ("frog_cyan").GetComponent<Animator> ().enabled = false; 
			GameObject.Find ("frog_green").GetComponent<Animator> ().enabled = false; 

			mTrackableBehaviour = GetComponent<TrackableBehaviour>();
			if (mTrackableBehaviour)
			{
				mTrackableBehaviour.RegisterTrackableEventHandler(this);
			}
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
			
			Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
			Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);
			
			// Enable rendering:
			foreach (Renderer component in rendererComponents)
			{
				component.enabled = true;
			}
			
			// Enable colliders:
			foreach (Collider component in colliderComponents)
			{
				component.enabled = true;
			}
			
			switch (mTrackableBehaviour.TrackableName) {
			case "FrameMarker1":
				Debug.Log(switchCharacters.animalName);
				GameObject.Find(switchCharacters.animalName+ "_magenta").GetComponent<Animator>().enabled = true;
				break;
			case "FrameMarker3":
				GameObject.Find(switchCharacters.animalName+"_green").GetComponent<Animator>().enabled = true;
				break;
			case "FrameMarker0":
				GameObject.Find(switchCharacters.animalName+"_cyan").GetComponent<Animator>().enabled = true;
				break;
			case "FrameMarker2":
				GameObject.Find(switchCharacters.animalName+"_yellow").GetComponent<Rigidbody2D>().isKinematic = false;
				GameObject.Find(switchCharacters.animalName+"_yellow").GetComponent<BoxCollider2D>().enabled = true;
				break;
			}
			
			Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
		}
		
		
		private void OnTrackingLost()
		{
			Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
			Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);
			
			// Disable rendering:
			foreach (Renderer component in rendererComponents)
			{
				component.enabled = false;
			}
			
			// Disable colliders:
			foreach (Collider component in colliderComponents)
			{
				component.enabled = false;
			}
			
			if(found == true){
				switch (mTrackableBehaviour.TrackableName) {
				case "FrameMarker1":
					GameObject.Find(switchCharacters.animalName+ "_magenta").GetComponent<Animator>().enabled = false;
					break;
				case "FrameMarker3":
					GameObject.Find(switchCharacters.animalName+"_green").GetComponent<Animator>().enabled = false;
					break;
				case "FrameMarker0":
					GameObject.Find(switchCharacters.animalName+"_cyan").GetComponent<Animator>().enabled = false;
					break;
				case "FrameMarker2":
					GameObject.Find(switchCharacters.animalName+"_yellow").GetComponent<Rigidbody2D>().isKinematic = true;
					GameObject.Find(switchCharacters.animalName+"_yellow").GetComponent<BoxCollider2D>().enabled = false;
					break;
				}
			}
			
			Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
		}
		
		#endregion // PRIVATE_METHODS
	}
}

