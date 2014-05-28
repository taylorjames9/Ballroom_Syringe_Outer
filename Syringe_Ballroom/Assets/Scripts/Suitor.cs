using UnityEngine;
using System.Collections;

public class Suitor : MonoBehaviour {

	public GameObject mySpeech;

	// Use this for initialization
	void Start () {
		mySpeech.SetActive(false);
	}

	// Update is called once per frame
	void Update () {

	}
	void OnCollisionEnter2D(Collision2D other){

		StartCoroutine ("PauseAndSpeak");

	}

	void OnCollisionExit2D(Collision2D other){
		StartCoroutine ("PauseAndSilence");
	}

	IEnumerator PauseAndSpeak (){
		yield return new WaitForSeconds (2.0f);
		mySpeech.SetActive(true);
	}

	IEnumerator PauseAndSilence (){
		yield return new WaitForSeconds (1.5f);
		mySpeech.SetActive(false);
	}
}
