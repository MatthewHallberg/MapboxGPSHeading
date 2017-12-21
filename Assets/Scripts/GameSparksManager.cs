using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSparksManager : MonoBehaviour {

	/// <summary>The GameSparks Manager singleton</summary>
	private static GameSparksManager instance = null;
	void Awake() {
		if (instance == null) // check to see if the instance has a reference
		{
			instance = this; // if not, give it a reference to this class...
			DontDestroyOnLoad(this.gameObject); // and make this object persistent as we load new scenes
		} else // if we already have a reference then remove the extra manager from the scene
		{
			Destroy(this.gameObject);
		}
	}
}
