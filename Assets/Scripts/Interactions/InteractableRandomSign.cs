using UnityEngine;
using System.Collections;

public class InteractableRandomSign : Interactable {

    string myText;
    bool seen = false;

    void Awake() {
        myText = Database.Information.GetRandomStatement();
    }

    public override void OnInteract(Character character) {
        if (DialogBox.IsVisible()) {
            DialogBox.Hide();
            character.Behavior.setFrozen(false, true);
        } else {
            if (!seen) {
                character.GetComponent<CharacterBehavior>().GainPoints(3);
                seen = true;
            }
            DialogBox.Show(myText);
            character.Behavior.setFrozen(true, true);
        }
    }


}
