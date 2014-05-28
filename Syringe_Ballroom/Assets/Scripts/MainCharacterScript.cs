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
		public GameObject myStabText;
		public static bool stabLoverOption;
		public static bool stabSuitorOption;

		public enum CurrentMainCharAnimationState {Idle, WalkLeft, WalkRight, WalkLeftSyringe, WalkRightSyringe, Stab, Die};
		public static CurrentMainCharAnimationState currentMainAnimState;

		void Start(){
				noSyringe = true;
				animator = this.GetComponent<Animator>();
				myStabText.SetActive(false);
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
						else if (target.x < transform.position.x && holdingSyringe) {
								animator.SetInteger ("Direction",4);
								//currentMainAnimState = CurrentMainCharAnimationState.WalkLeftSyringe;
						}
						else if (target.x > transform.position.x && holdingSyringe) {
								animator.SetInteger ("Direction",5);
								//currentMainAnimState = CurrentMainCharAnimationState.WalkRightSyringe;
						}

						//animator.SetInteger ("Direction", 0);

						if (Vector3.Distance (transform.position, target) < 0.1f) {
								print ("Should be less than 0.1f away from target");
								if(noSyringe)
									animator.SetInteger ("Direction", 0);
								else
										animator.SetInteger ("Direction", 3);
						}

						//animation.Stop();

						yield return null;
				}
		}

		/*void Update(){
				if (Vector3.Distance (transform.position, target) < 0.1f) {
						print ("Should be less than 0.1f away from target");
						animator.SetInteger ("Direction", 0);
						//animation.Stop();
				}

		}*/
				
		void OnCollisionEnter2D(Collision2D other){
				print ("Should be entering collider and stopping");

				if (other.gameObject.tag == "syringe") {
						other.gameObject.SetActive (false);
						noSyringe = false;
						holdingSyringe = true; 
		} else if (other.gameObject.tag == "LoveInterest" && holdingSyringe == true) {

					myStabText.SetActive(false);

				}

				if(noSyringe)
						animator.SetInteger ("Direction", 0);
				else
						animator.SetInteger ("Direction", 3);
				//animation.Stop();
				StopCoroutine("Movement");

		}

		void OnCollisionStay2D(Collision2D other){
				print ("Should be staying!");
				if(noSyringe)
						animator.SetInteger ("Direction", 0);
				else if(holdingSyringe)
						animator.SetInteger ("Direction", 3);
				//animation.Stop();
				StopCoroutine("Movement");

			if (other.gameObject.tag == "LoveInterest" && holdingSyringe == true) {
				myStabText.SetActive (true);
						stabLoverOption = true;
				} 
			if (other.gameObject.tag == "Suitor1" && holdingSyringe == true) {
				myStabText.SetActive (true);
				stabSuitorOption = true;
				} 
		}

	void OnCollisionExit2D(Collision2D other){
		myStabText.SetActive (false);
				stabSuitorOption = false;
				stabLoverOption = false;
	}


}
