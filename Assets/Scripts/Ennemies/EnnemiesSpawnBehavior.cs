using UnityEngine;
using System.Collections;

public class EnnemiesSpawnBehavior : MonoBehaviour {

    Object[] ennemiesList;
    GameObject player;
    float width;
    float height;
    
    void Awake() {
        ennemiesList = Resources.LoadAll("Ennemies");
        player = GameObject.FindGameObjectWithTag("Player");
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;

    }

    void Start () {
	}
	


    void SummonEnnemy() {
        Object randomEnnemy = ennemiesList[Random.Range(0, ennemiesList.Length)];
        GameObject ennemy = Instantiate(randomEnnemy) as GameObject;

        Vector3 myPosition = player.transform.position;
        if (Random.value < 0.5)
            myPosition += new Vector3(width + 2, 0, 0) ;
        else
            myPosition -= new Vector3(width + 2, 0, 0);
        myPosition += new Vector3(0, Random.Range(-0.8f*height, 0.8f*height), 0);
        ennemy.transform.position = myPosition;
    }

}
