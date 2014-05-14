using UnityEngine;
using System.Collections;

public class ExitScript : MonoBehaviour {

		public GameObject main_cameraObject; 
		public GameObject second_cameraObject; 

		void OnCollisionEnter2D(Collision2D other){
				main_cameraObject.camera.enabled = false;
				second_cameraObject.camera.enabled = true;
		}
}
