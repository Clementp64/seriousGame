using UnityEngine;
using System.Collections;

public class ExitBehavior : MonoBehaviour {

    public GameObject ScoreMenu;

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag != "Player") return;
        collider.GetComponent<CharacterBehavior>().setFrozen(true, false);
        ScoreMenu.SetActive(true);
        ScoreMenu.GetComponent<GameOverUI>().SetTitle("Niveau terminé !");

    }


}
