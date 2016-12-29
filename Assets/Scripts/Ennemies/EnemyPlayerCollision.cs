using UnityEngine;
using System.Collections;


public class EnemyPlayerCollision : MonoBehaviour {


	// Use this for initialization
	void OnTriggerEnter2D(Collider2D collider) {
        if (GetComponentInParent<EnemyBehavior>().IsStunned()) return;
        if (!GetComponentInParent<EnemyBehavior>().isAwake) return;
        if (collider.tag == "Player")
            GetComponentInParent<EnemyBehavior>().OnHitCharacter();
    }
}

