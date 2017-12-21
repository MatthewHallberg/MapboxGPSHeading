namespace Mapbox.Unity.Ar
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;

	public class UIBehavior : MonoBehaviour {
		/// <summary>
		/// This class houses functions that get called when buttons are pressed 
		/// in the UI.
		/// </summary>
		private static UIBehavior _instance;
		public static UIBehavior Instance { get { return _instance; } } 

		public GameObject HomeScreen;
		public GameObject MessageScreen;
		public Text messageText;

		void Awake(){
			_instance = this;
			HomeScreen.SetActive (false);
			MessageScreen.SetActive (false);
		}

		public void ShowUI(){
			HomeScreen.SetActive (true);
		}

		public void RemoveButtonDown(){
			HomeScreen.SetActive (false);
			MessageService.Instance.RemoveAllMessages ();
			ARMessageProvider.Instance.RemoveCurrentMessages ();
			StartCoroutine (DelayRemoveRoutine ());
		}

		IEnumerator DelayRemoveRoutine(){
			yield return new WaitForSeconds (2f);
			HomeScreen.SetActive (true);
		}

		public void MessageButtonDown(){
			HomeScreen.SetActive (false);
			MessageScreen.SetActive (true);
		}

		public void SubmitButtonDown(){
			double lat = ARMessageProvider.Instance.deviceLocation._currentLocation.LatitudeLongitude.x;
			double lon = ARMessageProvider.Instance.deviceLocation._currentLocation.LatitudeLongitude.y;

			MessageService.Instance.SaveMessage (lat, lon, messageText.text);

			messageText.text = "";
			HomeScreen.SetActive (true);
			MessageScreen.SetActive (false);
			StartCoroutine (DelayLoadMessagesRoutine ());
		}

		IEnumerator DelayLoadMessagesRoutine(){
			yield return new WaitForSeconds (1f);
			MessageService.Instance.LoadAllMessages ();
		}
	}
}
