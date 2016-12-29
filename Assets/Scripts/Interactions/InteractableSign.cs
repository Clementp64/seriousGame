using UnityEngine;
using System.Collections;

public class InteractableSign : Interactable {

    public string myText;

    public override void OnInteract(Character character) {
        if (DialogBox.IsVisible()) {
            DialogBox.Hide();
            character.Behavior.setFrozen(false, true);
        } else {
            DialogBox.Show(myText.Replace("NEWLINE", "\n"));
            character.Behavior.setFrozen(true, true);
        }
    }


}
