using UnityEngine;
using System.Collections;

public class MainCharacterScript : MonoBehaviour {


		public float smoothing = 1f;
		public float speed;
		public GameObject stabbyPose;
		public Vector3 Target
		{
				get{ return target; }set
				{
						target = value;

						StopCoroutine("Movement");
						StartCoroutine("Movement", target);
				}
		}

		private Vector3 target;

		IEnumerator Movement (Vector3 target)
		{
				while(Vector3.Distance(transform.position, target) > 0.05f)
				{
						float step = speed * Time.deltaTime;
						//transform.position = Vector3.Lerp(transform.position, target, smoothing * Time.deltaTime);
						transform.position = Vector3.MoveTowards(transform.position, target, step);
						yield return null;
				}
		}

		void OnCollisionEnter2D(Collision2D other){
				//print ("Should be stopping");

				if (other.gameObject.tag == "syringe") {
						other.gameObject.SetActive (false);
						renderer.enabled = false;
						stabbyPose.renderer.enabled = true;

				}

				StopCoroutine("Movement");
		}

		void OnCollisionStay2D(Collision2D other){
				//print ("Should be stopping");
				StopCoroutine("Movement");
		}


}
