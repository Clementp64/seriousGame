using UnityEngine;
using System.Collections;

public class EnemyAggro : MonoBehaviour {



    void OnTriggerEnter2D(Collider2D collider) {
        return;
        /*
        if (collider.tag == "Player")
            GetComponentInParent<EnemyBehavior>().Awaken();
            */
    }


    void OnTriggerExit2D(Collider2D collider) {
        return;
        /*
        if (collider.tag == "Player")
            GetComponentInParent<EnemyBehavior>().Sleep();
            */
    }


}
