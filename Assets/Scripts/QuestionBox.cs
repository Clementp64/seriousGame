using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuestionBox : MonoBehaviour {

    static QuestionBox Instance;
    Image dialogFrame;
    Text dialogText;
    Image answersFrame;
    Text[] answerText = new Text[3];
    int selectedAnswer;
    int correctAnswer;
    Image selectionRectangle;
    bool isFrozen = false;
    bool isChecked = false;
    AudioSource audio;

    void Awake() {
        Instance = this;
        dialogFrame = GetComponent<Image>();
        dialogText = GetComponentInChildren<Text>();
        answersFrame = transform.Find("AnswerBox").gameObject.GetComponent<Image>();
        answerText[0] = transform.Find("AnswerBox").Find("Text1").gameObject.GetComponent<Text>();
        answerText[1] = transform.Find("AnswerBox").Find("Text2").gameObject.GetComponent<Text>();
        answerText[2] = transform.Find("AnswerBox").Find("Text3").gameObject.GetComponent<Text>();
        selectionRectangle = transform.Find("AnswerBox").Find("SelectionRectangle").GetComponent<Image>();
        selectedAnswer = 0;
        audio = gameObject.AddComponent<AudioSource>();
        audio.playOnAwake = false;
        audio.clip = (AudioClip)Resources.Load("Sounds/wrong");
    }

    public static void ShowQuestion(string[] displayText) {
        Instance.DoShow(displayText[0]);
        Instance.ShowAnswers(displayText);
    }

    public static void LowerSelection() {
        Instance.DoLowerSelection();
    }

    void DoLowerSelection() {
        if (selectedAnswer == 2) return;
        if (isFrozen) return;
        selectedAnswer++;
        selectionRectangle.GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, 75);
    }

    public static void GoUpSelection() {
        Instance.DoGoUpSelection();
    }

    void DoGoUpSelection() {
        if (selectedAnswer == 0) return;
        if (isFrozen) return;
        selectedAnswer--;
        selectionRectangle.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 75);
    }

    void DoShow(string displayText) {
        dialogFrame.enabled = true;
        dialogText.text = displayText;
        dialogText.enabled = true;
    }

    void ShowAnswers(string[] displayText) {
        answersFrame.enabled = true;
        for (int i = 0; i < 3; i++) {
            answerText[i].text = displayText[i+1];
            answerText[i].enabled = true;
            answerText[i].color = Color.white;
        }
        correctAnswer = int.Parse(displayText[4]);

        if (!isChecked) {
            selectionRectangle.enabled = true;
            DoGoUpSelection();
            DoGoUpSelection();
        } else {
            answerText[selectedAnswer].color = Color.red;
            answerText[correctAnswer].color = Color.green;
        }
    }

    public static void CheckAnswer() {
        Instance.DoCheckAnswer();
    }

    void DoCheckAnswer() {
        if (isChecked) return;
        isChecked = true;
        isFrozen = true;
        if(selectedAnswer == correctAnswer)
            GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehavior>().GainPoints(5);
        else {
            answerText[selectedAnswer].color = Color.red;
            audio.Play();
        }
        answerText[correctAnswer].color = Color.green;
        StartCoroutine(HideAfterDelay(1.5f));

    }

    IEnumerator HideAfterDelay(float delay) {

        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + delay) {
            yield return null;
        }

        DoHide();
        isFrozen = false;

    }

    public static void Hide() {
        Instance.DoHide();
    }

    public void DoHide() {
        GetComponent<Image>().enabled = false;
        GetComponentInChildren<Text>().enabled = false;
        answersFrame.enabled = false;
        for (int i = 0; i < 3; i++)
            answerText[i].enabled = false;
        selectionRectangle.enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehavior>().setFrozen(false, true);
    }

    public static bool IsVisible() {
        return Instance.dialogFrame.enabled;
    }

    public static void Uncheck() {
        Instance.InstanceUncheck();
    }

    void InstanceUncheck() {
        isChecked = false;
    }

}
