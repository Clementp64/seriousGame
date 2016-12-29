using UnityEngine;
using System.Collections;

public class heartObject : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "Player") {
			collider.GetComponentInParent<CharacterBehavior> ().gainHp ();
			Destroy(gameObject);
		}
	}
		
}
