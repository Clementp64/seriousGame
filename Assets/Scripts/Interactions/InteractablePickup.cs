using UnityEngine;
using System.Collections;

public class InteractablePickup : Interactable {

    public TrashType TypeOfTrash;
    private bool authorizePickup = true;

    public override void OnInteract(Character character) {
        if (!authorizePickup) return;
        character.GetComponent<CharacterBehavior>().CarryObject(this);
        
        BroadcastMessage("OnPickupObject", character, SendMessageOptions.DontRequireReceiver);

    }

    public void SetAuthorizePickup(bool b) {
        authorizePickup = b;
    }


}
