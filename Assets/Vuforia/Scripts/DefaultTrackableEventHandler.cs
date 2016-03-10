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
	public class DefaultTrackableEventHandler : MonoBehaviour,
	ITrackableEventHandler
	{
		#region PRIVATE_MEMBER_VARIABLES
		
		private TrackableBehaviour mTrackableBehaviour;
		
		#endregion // PRIVATE_MEMBER_VARIABLES
		
		#region UNTIY_MONOBEHAVIOUR_METHODS
		
		void Start()
		{
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
				if (GameObject.Find ("cubey_magenta").GetComponent<Rigidbody2D> () != null)
					GameObject.Find ("cubey_magenta").GetComponent<Rigidbody2D> ().isKinematic = false;
				break;
			case "FrameMarker3":
				if (GameObject.Find ("cubey_green").GetComponent<Rigidbody2D> () != null)
					GameObject.Find ("cubey_green").GetComponent<Rigidbody2D> ().isKinematic = false;
				break;
			case "FrameMarker0":
				if (GameObject.Find ("cubey_cyan").GetComponent<Rigidbody2D> () != null)
					GameObject.Find ("cubey_cyan").GetComponent<Rigidbody2D> ().isKinematic = false;
				break;
			case "FrameMarker2":
				if (GameObject.Find ("cubey_yellow").GetComponent<Rigidbody2D> () != null)	
					GameObject.Find ("cubey_yellow").GetComponent<Rigidbody2D> ().isKinematic = false;
				break;
			case "FrameMarker4":
				if(GameObject.Find("cubey_dblue") !=null){
					if (GameObject.Find ("cubey_dblue").GetComponent<Rigidbody2D> () != null)
						GameObject.Find ("cubey_dblue").GetComponent<Rigidbody2D> ().isKinematic = false;
				}
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
			
			Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
		}
		
		#endregion // PRIVATE_METHODS
	}
}
