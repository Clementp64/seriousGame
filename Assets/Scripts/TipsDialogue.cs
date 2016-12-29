using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class TipsDialogue : MonoBehaviour {

    static TipsDialogue Instance;
    Image tipsFrame;
    Text tipsText;
    List<string> tipsList = new List<string>();
    Animator anim;
    bool isShowing = false;

    void Awake() {
        Instance = this;
        tipsFrame = GetComponent<Image>();
        tipsText = GetComponentInChildren<Text>();
        anim = GetComponent<Animator>();
    }

    public static void Show(string displayText) {
        Instance.DoShow(displayText);
    }

    void DoShow(string displayText) {
        if (!tipsList.Contains(displayText)) tipsList.Add(displayText);
        if (isShowing) return;
        isShowing = true;
        tipsFrame.enabled = true;
        tipsText.text = tipsList[0];
        tipsList.RemoveAt(0);
        tipsText.enabled = true;
        anim.SetTrigger("MoveUp");
        StartCoroutine(HideAfterDelay(3));
    }

    public static void Hide() {
        Instance.DoHide();
    }

    public void DoHide() {
        GetComponent<Image>().enabled = false;
        GetComponentInChildren<Text>().enabled = false;
    }

    IEnumerator HideAfterDelay(float delay) {
        yield return new WaitForSeconds(delay);
        anim.SetTrigger("MoveDown");
        yield return new WaitForSeconds(1.5f);
        isShowing = false;
        CheckNextTip();
    }

    public void CheckNextTip() {
        if (tipsList.Count > 0) Show(tipsList[0]);
    }

}
