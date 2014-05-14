using UnityEngine;
using System.Collections;

public class LoverScript : MonoBehaviour {


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
				//print ("The love interest was just collided with");

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
