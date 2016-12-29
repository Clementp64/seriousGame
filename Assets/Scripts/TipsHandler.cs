using UnityEngine;
using System.Collections;

public class TipsHandler : MonoBehaviour {

    static TipsHandler Instance;

    bool recyclingPoints = false;
    bool ennemies = false;
    bool pickup = false;
    bool recycle = false;

    void Awake() {
        Instance = this;
    }

    void Start() {
        StartCoroutine(TipAfterDelay(1, "Utilise les flèches pour te déplacer"));
        StartCoroutine(TipAfterDelay(7, "Appuie sur 'C' pour attaquer"));
    }

    public static void Ennemies() {
        Instance.InstanceEnnemies();
    }

    void InstanceEnnemies() {
        if (recyclingPoints) return;
        recyclingPoints = true;
        TipsDialogue.Show("Attaque les déchets pour les assommer");

    }

    public static void Recycle() {
        Instance.InstanceRecycle();
    }

    void InstanceRecycle() {
        if (recycle) return;
        recycle = true;
        TipsDialogue.Show("Dépose le déchet dans la bonne poubelle pour le recycler");
    }

    public static void PickUp() {
        Instance.InstancePickup();
    }

    void InstancePickup() {
        if (pickup) return;
        pickup = true;
        TipsDialogue.Show("Approche d'un déchet assommé et appuie sur 'Espace' pour le porter");
    }

    public static void RecyclingPoints() {
        Instance.InstanceRecyclingPoints();
    }

    void InstanceRecyclingPoints() {
        if (ennemies) return;
        ennemies = true;
        TipsDialogue.Show("Gagne des points de recyclage pour acheter des améliorations");

    }

    IEnumerator TipAfterDelay(float delay, string text) {
        yield return new WaitForSeconds(delay);
        TipsDialogue.Show(text);
    }

}
