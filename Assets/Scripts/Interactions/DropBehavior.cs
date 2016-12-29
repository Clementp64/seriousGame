using UnityEngine;
using System.Collections;

public class DropBehavior : MonoBehaviour {

    public TrashType TypeOfTrash;
    public GameObject RecyclingEffect;
    public GameObject WrongEffect;

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Player") {
            float myAngle = Vector3.Angle(collider.GetComponent<CharacterBehavior>().vectMovement, collider.transform.position - transform.position);
            if (myAngle < 140) return;
            GameObject carriedObject = collider.GetComponent<CharacterBehavior>().GetCarriedObject();
            if(carriedObject != null) {
                collider.GetComponent<CharacterBehavior>().DropObject();
                if (carriedObject.GetComponent<InteractablePickup>().TypeOfTrash == TypeOfTrash) {
					collider.GetComponent<CharacterBehavior>().GainPoints(carriedObject.GetComponent<EnemyBehavior>().score);
                    carriedObject.GetComponent<EnemyBehavior>().OnDeath();
                    if (RecyclingEffect != null) {
                        GameObject myEffect = (GameObject)Instantiate(RecyclingEffect);
                        myEffect.transform.position = transform.position;
                    }
                }else {
                    EnemyBehavior behavior = carriedObject.GetComponent<EnemyBehavior>();
                    if (behavior != null) behavior.MakeStronger();
                    //collider.GetComponent<CharacterBehavior>().LosePoints();
                    if(WrongEffect != null) {
                        GameObject myEffect = (GameObject)Instantiate(WrongEffect);
                        myEffect.transform.position = transform.position;

                    }
                }
            }
        }
    }

}
