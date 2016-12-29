using UnityEngine;
using System.Collections;

public class Database {

    private static ItemsDatabase myItems;
    private static InformationDatabase myInformation;
    public static ItemsDatabase Items {
        get {
            if(myItems == null) {
                myItems = Resources.Load<ItemsDatabase>("Databases/ItemsDatabase"); 
            }
            return myItems;
        }
    }
    public static InformationDatabase Information {
        get {
            if(myInformation == null) {
                myInformation = Resources.Load<InformationDatabase>("Databases/InformationDatabase");
            }
            return myInformation;
        }
    }


    void TestMethod() {
        Debug.Log(Database.Items);
    }

}
