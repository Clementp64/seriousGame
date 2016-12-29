using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {

	public virtual void OnInteract(Character player) {
        Debug.Log("OnInteract is not implemented");
    }

}
