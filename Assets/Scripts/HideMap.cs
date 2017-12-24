using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMap : MonoBehaviour {

	// Use this for initialization
	void Start () {
		HideMapCoroutine = StartCoroutine (HideMapRoutine ());
	}

	Coroutine HideMapCoroutine;
	IEnumerator HideMapRoutine(){
		while (true) {
			yield return new WaitForEndOfFrame ();
			if (transform.childCount > 5) {
				DisableChildren ();
			}
		}
	}

	void DisableChildren(){
		StopCoroutine(HideMapCoroutine);
		foreach (Transform child in this.transform) {
			if (child.GetComponent<MeshRenderer>() != null || child.GetComponent<LineRenderer>() != null) {
				child.gameObject.SetActive (false);
			}
		}
	}
}
