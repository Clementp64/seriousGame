using UnityEngine;
using System.Collections;

public class BinHelp : Interactable {

	public string myText;

	public override void OnInteract(Character character) {
		if (BinBox.IsVisible()) {
			BinBox.Hide();
			character.Behavior.setFrozen(false, true);
		} else {
			//myText = ("Poubelle green");
			BinBox.Show(myText);
			character.Behavior.setFrozen(true, true);
		}
	}


}
