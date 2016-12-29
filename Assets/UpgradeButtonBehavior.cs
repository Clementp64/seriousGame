using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class UpgradeButtonBehavior : MonoBehaviour, ISelectHandler, IDeselectHandler {

    public int index;

    void Awake() {
        index = transform.GetSiblingIndex();
    }

    public void SetColor(Color col) {
        transform.Find("Cost").Find("RecyclingText").gameObject.GetComponent<Text>().color = col;

    }

    public void OnSelect(BaseEventData eventData) {
        Upgrades.ButtonSelected(this);
    }

    public void OnDeselect(BaseEventData eventData) {
        SetColor(new Color(1, 1, 1, 0.39f));

    }
}
