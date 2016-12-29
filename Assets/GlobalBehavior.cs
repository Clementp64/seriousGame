using UnityEngine;
using System.Collections;

public class GlobalBehavior : MonoBehaviour {
    
	void Start () {
        Database.Information.Initialize();
	}
}
