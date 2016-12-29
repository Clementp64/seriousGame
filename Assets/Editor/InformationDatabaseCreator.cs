using UnityEngine;
using UnityEditor;
using System.Collections;

public class InformationDatabaseCreator : MonoBehaviour {

    [MenuItem("Serious Games/Databases/Create Information Database")]
    public static void CreateInformationDatabase() {

        InformationDatabase newDatabase = ScriptableObject.CreateInstance<InformationDatabase>();
        AssetDatabase.CreateAsset(newDatabase, "Assets/InformationDatabase.asset");
        AssetDatabase.Refresh();

    }

}
