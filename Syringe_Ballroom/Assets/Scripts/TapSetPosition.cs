using UnityEngine;
using System.Collections;

public class TapSetPosition : MonoBehaviour {

		public MainCharacterScript mainCharScript;


		void OnTap(TapGesture gesture) { 	

				Vector3 newTarget = GetWorldPos( gesture.Position );
				mainCharScript.Target = newTarget; 
		
		}

		// Convert from screen-space coordinates to world-space coordinates on the Z = 0 plane
		public static Vector3 GetWorldPos( Vector2 screenPos )
		{
				Ray ray = Camera.main.ScreenPointToRay( screenPos );

				// we solve for intersection with z = 0 plane
				float t = -ray.origin.z / ray.direction.z;

				return ray.GetPoint( t );
		}
}
