using UnityEngine;
using System.Collections;

public class InteractableChest : Interactable {

    public Sprite openChestSprite;
    public ItemType itemInChest;
    public int amount;
    bool isOpen = false;

    public override void OnInteract(Character character) {
        if (!isOpen) {
            character.Inventory.AddItem(itemInChest, amount);
            GetComponentInChildren<SpriteRenderer>().sprite = openChestSprite;
            isOpen = true;

        }
    }

}
