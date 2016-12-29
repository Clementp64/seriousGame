using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour {

    static DialogBox Instance;
    Image dialogFrame;
    Text dialogText;

    void Awake() {
        Instance = this;
        dialogFrame = GetComponent<Image>();
        dialogText = GetComponentInChildren<Text>();
    }

    public static void Show(string displayText) {
        Instance.DoShow(displayText);
    }

    void DoShow(string displayText) {
        dialogFrame.enabled = true;
        dialogText.text = displayText;
        dialogText.enabled = true;
    }

    public static void Hide() {
        Instance.DoHide();
    }

    public void DoHide() {
        GetComponent<Image>().enabled = false;
        GetComponentInChildren<Text>().enabled = false;
    }

    public static bool IsVisible() {
        return Instance.dialogFrame.enabled;
    }

}
