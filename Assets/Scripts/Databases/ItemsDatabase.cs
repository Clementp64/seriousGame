using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemsDatabase : ScriptableObject {

    public List<ItemData> Data;
    public ItemData FindItem(ItemType itemType) {
        for(int i=0; i<Data.Count; i++) {
            if(Data[i].Type == itemType)
                return (Data[i]);
        }
        return null;
    }
}

[System.Serializable]
public class ItemData {
    public ItemType Type;
    public GameObject Prefab;
    public EquipType Equip;
    public PickupAnimation Animation;

    public enum PickupAnimation {
        None,
        OneHanded,
        TwoHanded,
    }
    public enum EquipType {
        NotEquipable,
        SwordHand,
        ShieldHand,
    }
}