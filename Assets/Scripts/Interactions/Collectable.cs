using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour {

    public ItemType itemType;
    public int amount;

	void OnTriggerEnter2D(Collider2D collider) {
        CharacterInventoryModel inventory = collider.GetComponent<CharacterInventoryModel>();
        if (inventory == null) return;
        inventory.AddItem(itemType, amount);
        Destroy(gameObject);
    }
}
