using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerHpBehavior : MonoBehaviour {

    public GameObject Heart;
    public Sprite FullHeartSprite;
    public Sprite HalfHeartSprite;
    public Sprite EmptyHeartSprite;
    private List<GameObject> myHearts = new List<GameObject>();
    private float myMaxHp;

    public void FirstDisplay(float maxHp, float currentHp) {
        myMaxHp = maxHp;
        for (int i = 0; i < maxHp; i++) {
            GameObject Heart1 = Instantiate(Heart);
            myHearts.Add(Heart1);
            Heart1.transform.SetParent(this.transform, false);
            Heart1.GetComponent<RectTransform>().anchoredPosition += new Vector2(i * 45, 0);
            Heart1.GetComponent<Image>().sprite = FullHeartSprite;
        }
        for (int i = 0; i < maxHp - Mathf.Floor(currentHp); i++) {
            myHearts[(int)maxHp - 1 - i].GetComponent<Image>().sprite = EmptyHeartSprite;
        }
        if (!Mathf.Approximately(currentHp, Mathf.RoundToInt(currentHp)))
            myHearts[(int)Mathf.Ceil(currentHp)].GetComponent<Image>().sprite = HalfHeartSprite;
    }

    public void UpdateDisplay(float maxHp, float currentHp) {
        if (currentHp < 0) return;
        if (maxHp > myMaxHp) {
            for (int i = (int)myMaxHp; i < maxHp; i++) {
                GameObject Heart1 = Instantiate(Heart);
                myHearts.Add(Heart1);
                Heart1.transform.SetParent(this.transform, false);
                Heart1.GetComponent<RectTransform>().anchoredPosition += new Vector2(i * 45, 0);
                Heart1.GetComponent<Image>().sprite = EmptyHeartSprite;
            }
            myMaxHp = maxHp;
        }
        if (myHearts.Count == 0) {
            FirstDisplay(maxHp, currentHp);
            return;
        }

        for(int i=0; i<Mathf.Ceil(currentHp); i++) {
            myHearts[i].GetComponent<Image>().sprite = FullHeartSprite;
        }

        for(int i = 0; i < maxHp - Mathf.Floor(currentHp); i++) {
            myHearts[(int)maxHp -1 - i].GetComponent<Image>().sprite = EmptyHeartSprite;
        }

        if(!Mathf.Approximately(currentHp, Mathf.RoundToInt(currentHp))) {
            myHearts[(int)Mathf.Ceil(currentHp) - 1].GetComponent<Image>().sprite = HalfHeartSprite;

        }
    }


}
