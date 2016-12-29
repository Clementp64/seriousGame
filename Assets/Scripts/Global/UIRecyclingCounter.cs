using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIRecyclingCounter : MonoBehaviour {

    public CharacterInventoryModel Inventory;
    Text myText;

	void Awake () {
        myText = GetComponent<Text>();
	}
	
	void Update () {
        myText.text = Inventory.GetItemCount(ItemType.RecyclingPoints).ToString("000");
	}
}
