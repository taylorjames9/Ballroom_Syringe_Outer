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
				while(Vector3.Distance(transform.position, target) > 0.05f)
				{
						float step = speed * Time.deltaTime;
						//transform.position = Vector3.Lerp(transform.position, target, smoothing * Time.deltaTime);
						transform.position = Vector3.MoveTowards(transform.position, target, step);

						if (target.x < transform.position.x && noSyringe) {
								currentMainAnimState = CurrentMainCharAnimationState.WalkLeft;

						} else if (target.x > transform.position.x && noSyringe) {
								currentMainAnimState = CurrentMainCharAnimationState.WalkRight;
						}
						else if (target.x < transform.position.x && holdingSyringe) {
								currentMainAnimState = CurrentMainCharAnimationState.WalkLeftSyringe;
						}
						else if (target.x > transform.position.x && holdingSyringe) {
								currentMainAnimState = CurrentMainCharAnimationState.WalkRightSyringe;
						}

						yield return null;
				}
		}

		void Update(){
			switch (currentMainAnimState) {
			case CurrentMainCharAnimationState.Idle:
					animator.SetInteger ("Direction",0);
					break;
			case CurrentMainCharAnimationState.WalkLeft:
					animator.SetInteger ("Direction",1);
					break;
			case CurrentMainCharAnimationState.WalkRight:
					animator.SetInteger ("Direction",2);
					break;
			case CurrentMainCharAnimationState.WalkLeftSyringe:
					animator.SetInteger ("Direction",3);
					break;
			case CurrentMainCharAnimationState.WalkRightSyringe:
					animator.SetInteger ("Direction", 4);
					break;
			case CurrentMainCharAnimationState.Stab:
					animator.SetInteger ("Direction", 5);
					break;
			case CurrentMainCharAnimationState.Die:
					animator.SetInteger ("Direction", 6);
					break;
			default:
					break;
			}
		}



		void OnCollisionEnter2D(Collision2D other){
				//print ("Should be stopping");

				if (other.gameObject.tag == "syringe") {
						other.gameObject.SetActive (false);
						//renderer.enabled = false;
						//stabbyPose.renderer.enabled = true;

						noSyringe = false;
						holdingSyringe = true; 
				}

				StopCoroutine("Movement");
		}

		void OnCollisionStay2D(Collision2D other){
				//print ("Should be stopping");
				StopCoroutine("Movement");
		}


}
