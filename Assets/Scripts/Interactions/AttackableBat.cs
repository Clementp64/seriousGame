using UnityEngine;
using System.Collections;

public class AttackableBat : Attackable {

    void Awake() {
    }

    public override void OnHit(ItemType item, Collider2D weaponCollider) {

        GetComponentInParent<EnemyBehavior>().OnHit(weaponCollider);

    }

}
