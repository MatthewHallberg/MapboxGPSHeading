namespace Mapbox.Unity.Ar
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using GameSparks.Core;

	public class MessageService : MonoBehaviour {

		/// <summary>
		/// This class handles communication with gamesparks for
		/// removing, loading, and writing new messages. New Message
		/// objects are instantiated here.
		/// </summary>
		private static MessageService _instance;
		public static MessageService Instance { get { return _instance; } } 

		public Transform mapRootTransform;

		public GameObject messagePrefabAR;

		void Awake(){
			_instance = this;
		}

		public void RemoveAllMessages(){
			new GameSparks.Api.Requests.LogEventRequest ()
				.SetEventKey ("REMOVE_MESSAGES")
				.Send ((response) => {
				if (!response.HasErrors) {
					Debug.Log ("Message Saved To GameSparks...");
				} else {
					Debug.Log ("Error Saving Message Data...");
				}
			});
		}

		public void LoadAllMessages(){

			List<GameObject> messageObjectList = new List<GameObject> ();
			
			new GameSparks.Api.Requests.LogEventRequest().SetEventKey("LOAD_MESSAGE").Send((response) => {
				if (!response.HasErrors) {
					Debug.Log("Received Player Data From GameSparks...");
					List<GSData> locations = response.ScriptData.GetGSDataList ("all_Messages");
					for (var e = locations.GetEnumerator (); e.MoveNext ();) {
						
						GameObject MessageBubble = Instantiate (messagePrefabAR,mapRootTransform);
						Message message = MessageBubble.GetComponent<Message>();

						message.latitude = double.Parse(e.Current.GetString ("messLat"));
						message.longitude = double.Parse(e.Current.GetString ("messLon"));
						message.text = e.Current.GetString ("messText");
						messageObjectList.Add(MessageBubble);
					}
				} else {
					Debug.Log("Error Loading Message Data...");
				}
			});
			//pass list of objects to ARmessage provider so they can be placed
			ARMessageProvider.Instance.LoadARMessages (messageObjectList);
		}

		public void SaveMessage(double lat, double lon, string text){
			new GameSparks.Api.Requests.LogEventRequest ()

				.SetEventKey ("SAVE_GEO_MESSAGE")
				.SetEventAttribute ("LAT", lat.ToString())
				.SetEventAttribute ("LON", lon.ToString())
				.SetEventAttribute ("TEXT", text)
				.Send ((response) => {
					
				if (!response.HasErrors) {
					Debug.Log ("Message Saved To GameSparks...");
				} else {
					Debug.Log ("Error Saving Message Data...");
				}
			});
		}
	}
}
