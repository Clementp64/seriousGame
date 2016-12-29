using UnityEngine;
using System.Collections;

public class AttackableBush : Attackable {

    public Sprite destroyedSprite;
    public GameObject DestroyEffect;
    private SpriteRenderer myRenderer;

    void Awake() {
        myRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public override void OnHit(ItemType item, Collider2D weaponCollider) {
        DestroyBush();
    }

    void DestroyBush() {
        if (GetComponent<Collider2D>() != null)
            GetComponent<Collider2D>().enabled = false;
        myRenderer.sprite = destroyedSprite;

        if (DestroyEffect != null) {
            GameObject destroyEffect = (GameObject)Instantiate(DestroyEffect);
            destroyEffect.transform.position = transform.position;
        }
    }

}
