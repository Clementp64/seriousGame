using UnityEngine;
using UnityEditor;
using System.Collections;

public class ItemsDatabaseCreator : MonoBehaviour {

    [MenuItem("Serious Games/Databases/Create Items Database")]
    public static void CreateItemsDatabase() {

        ItemsDatabase newDatabase = ScriptableObject.CreateInstance<ItemsDatabase>();
        AssetDatabase.CreateAsset(newDatabase, "Assets/ItemsDatabase.asset");
        AssetDatabase.Refresh();

    }

}
