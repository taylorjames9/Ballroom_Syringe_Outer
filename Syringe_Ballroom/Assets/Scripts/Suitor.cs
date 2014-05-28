using UnityEngine;
using System.Collections;

public class Suitor : MonoBehaviour {

	public GameObject mySpeech;
		private Animator animator;

	// Use this for initialization
	void Start () {
		mySpeech.SetActive(false);
		animator = this.GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {

	}
	void OnCollisionEnter2D(Collision2D other){

		StartCoroutine ("PauseAndSpeak");

	}

		void OnTap(TapGesture gesture) { 
				if (MainCharacterScript.stabSuitorOption) {
						//Play Death Animation
						animator.SetBool ("isDead", true);
						mySpeech.SetActive(false);
						//MainCharacterScript.myStabText.SetActive (false);
				}

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
