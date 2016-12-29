using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterInventoryModel : MonoBehaviour {

    Dictionary<ItemType, int> items = new Dictionary<ItemType, int>();
    private CharacterBehavior Behavior;

    void Awake() {
        Behavior = GetComponent<CharacterBehavior>();
    }
    
    void Start() {
        AddItem(ItemType.RecyclingPoints, 0);
    }

    public int GetItemCount(ItemType itemType) {
        if (!items.ContainsKey(itemType)) return 0;
        return items[itemType];
    }


    public void AddItem(ItemType itemType) {
        AddItem(itemType, 1);

    }

    public void AddItem(ItemType itemType, int amount) {
        if (items.ContainsKey(itemType)) {
            if (amount < -GetItemCount(itemType)) return;
            items[itemType] += amount;
        } else {
            items.Add(itemType, amount);
        }

        if(amount > 0) {
            ItemData itemData = Database.Items.FindItem(itemType);
            if(itemData != null) {
                if(itemData.Equip == ItemData.EquipType.SwordHand && Behavior.equipedWeapon == ItemType.None) {
                    Behavior.EquipWeapon(itemType);

                }
                if(itemData.Animation != ItemData.PickupAnimation.None)
                    Behavior.PreviewItem(itemType);
            }
        }

    }
   
}
