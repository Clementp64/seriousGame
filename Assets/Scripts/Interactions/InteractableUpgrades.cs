using UnityEngine;
using System.Collections;

public class InteractableUpgrades : Interactable {

    public override void OnInteract(Character character) {
        if (Upgrades.IsShowing()) return;
        Upgrades.Show();
        character.Behavior.setFrozen(true, true);
    }


}
