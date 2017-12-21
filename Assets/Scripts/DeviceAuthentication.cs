namespace Mapbox.Unity.Ar
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class DeviceAuthentication : MonoBehaviour {
		/// <summary>
		/// Authenticates our device with GameSparks. Once authenticated, we show the UI
		/// and load initial messages. 
		/// </summary>
		void Start () {
			StartCoroutine (DelayAuthenticateRoutine ());
		}

		IEnumerator DelayAuthenticateRoutine(){

			yield return new WaitForSeconds (1f);

			new GameSparks.Api.Requests.DeviceAuthenticationRequest().Send((response) => {
				if (!response.HasErrors) {
					Debug.Log("Device Authenticated...");
					//tell message provider we have been authenticated
					ARMessageProvider.Instance.deviceAuthenticated = true;
				} else {
					Debug.Log("Error Authenticating Device...");
				}
			});
		}
	}
}
