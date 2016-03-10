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
	public class ARTrackableEventHandler : MonoBehaviour,
	ITrackableEventHandler
	{
		#region PRIVATE_MEMBER_VARIABLES
		
		private TrackableBehaviour mTrackableBehaviour;
		
		private ArrayList items;
		

		#endregion // PRIVATE_MEMBER_VARIABLES
		
		#region UNTIY_MONOBEHAVIOUR_METHODS
		
		void Start()
		{
			GameObject.FindObjectOfType<VideoTextureRenderer> ().enabled = false;
			mTrackableBehaviour = GetComponent<TrackableBehaviour>();
			if (mTrackableBehaviour)
			{
				mTrackableBehaviour.RegisterTrackableEventHandler(this);
			}
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
			
			if (mTrackableBehaviour.TrackableName.Equals ("yellow")) {
				GameObject.Find("nose").GetComponent<AudioSource>().Play();
			} else if (mTrackableBehaviour.TrackableName.Equals ("aqua")) {
				GameObject.Find("boat").GetComponent<AudioSource>().Play();
			} else if (mTrackableBehaviour.TrackableName.Equals ("TRYME_magenta")) {
				GameObject.Find("cork").GetComponent<AudioSource>().Play();
			} else if (mTrackableBehaviour.TrackableName.Equals ("lime_fromcamera")) {
				GameObject.Find("baby_happy").GetComponent<AudioSource>().Play();
			} else if (mTrackableBehaviour.TrackableName.Equals ("darkblue")) {
				GameObject.Find("apple").GetComponent<AudioSource>().Play();
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
