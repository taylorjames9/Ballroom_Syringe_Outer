﻿using UnityEngine;
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
		private Animator animator;
		private bool noSyringe;
		private bool holdingSyringe;

		public enum CurrentMainCharAnimationState {Idle, WalkLeft, WalkRight, WalkLeftSyringe, WalkRightSyringe, Stab, Die};
		public static CurrentMainCharAnimationState currentMainAnimState;

		void Start(){
				noSyringe = true;
				animator = this.GetComponent<Animator>();
		}

		IEnumerator Movement (Vector3 target)
		{

				while(Vector3.Distance(transform.position, target) > 0.1f)
				{
						float step = speed * Time.deltaTime;
						//transform.position = Vector3.Lerp(transform.position, target, smoothing * Time.deltaTime);
						transform.position = Vector3.MoveTowards(transform.position, target, step);

						if (target.x < transform.position.x && noSyringe) {
								//currentMainAnimState = CurrentMainCharAnimationState.WalkLeft;
								animator.SetInteger ("Direction",1);

						} else if (target.x > transform.position.x && noSyringe) {
								//currentMainAnimState = CurrentMainCharAnimationState.WalkRight;
								animator.SetInteger ("Direction",2);
						}
						/*else if (target.x < transform.position.x && holdingSyringe) {
								//currentMainAnimState = CurrentMainCharAnimationState.WalkLeftSyringe;
						}
						else if (target.x > transform.position.x && holdingSyringe) {
								//currentMainAnimState = CurrentMainCharAnimationState.WalkRightSyringe;
						}*/
						//animator.SetInteger ("Direction", 0);

						yield return null;
				}



		}

		void Update(){
				if (Vector3.Distance (transform.position, target) < 0.1f) {
						print ("Should be less than 0.1f away from target");
						animator.SetInteger ("Direction", 0);
				}

		}
				
		void OnCollisionEnter2D(Collision2D other){
				print ("Should be entering collider and stopping");

				if (other.gameObject.tag == "syringe") {
						other.gameObject.SetActive (false);
						noSyringe = false;
						holdingSyringe = true; 
				}

				animator.SetInteger ("Direction", 0);
				StopCoroutine("Movement");

		}

		void OnCollisionStay2D(Collision2D other){
				print ("Should be staying!");
				animator.SetInteger ("Direction", 0);
				StopCoroutine("Movement");

		}
}
