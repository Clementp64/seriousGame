using UnityEngine;
using System.Collections;

public class InteractableQuestionSign : Interactable {

    string[] myText;
    bool isSeen = false;
    bool isChecked = false;

    void Start() {
        myText = Database.Information.GetRandomQuestion();
    }

    public override void OnInteract(Character character) {
        if (QuestionBox.IsVisible()) {
            if (isChecked)
                QuestionBox.Hide();
            else {
                isChecked = true;
                QuestionBox.CheckAnswer();
            }
        } else {
            if (!isSeen)
                QuestionBox.Uncheck();
            isSeen = true;
            QuestionBox.ShowQuestion(myText);
            character.Behavior.setFrozen(true, true);
        }
    }


}
