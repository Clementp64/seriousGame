using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent( typeof( CharacterBehavior))]

public class CharacterInteractionModel : MonoBehaviour {
    
    private Character character;

    void Awake() {
        character = GetComponent<Character>();
    }

    //when Interact key is pressed
    public void OnInteract() {

        //find closest interactable
        Interactable usableInteractable = FindUsableInteractable();

        //if any, interact with it
        if(usableInteractable != null)
            usableInteractable.OnInteract(character);

    }

    //return the closest interactable in front of the player in a circle radius
    Interactable FindUsableInteractable() {
        Collider2D[] closeColliders = Physics2D.OverlapCircleAll(transform.position, 0.9f);
        Interactable closestInteractable = null;
        float angleToClosestInteractable = Mathf.Infinity;

        //for each colider in the radius
        foreach (Collider2D closeCollider in closeColliders) {

            //if the object is not interactable, skip it
            Interactable colliderInteractable = closeCollider.GetComponent<Interactable>();
            if(colliderInteractable == null)
                continue;

            //compute angle to object
            Vector3 directionToInteractable = closeCollider.transform.position - transform.position;

            float angleToInteractable = Vector3.Angle(character.Behavior.GetDirection(), directionToInteractable);

            //interaction limited to objects with angle < 50°
            if (angleToInteractable < 50) {
                //keep the object with the lowest angle to the player
                if (angleToInteractable < angleToClosestInteractable) {
                    angleToClosestInteractable = angleToInteractable;
                    closestInteractable = colliderInteractable;
                }
            }
        }

        return closestInteractable;
    }

}
