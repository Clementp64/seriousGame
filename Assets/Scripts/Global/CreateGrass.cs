using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateGrass : MonoBehaviour {

    public GameObject grass1;
    public GameObject grass2;
    public GameObject flower1;
    public GameObject flower2;
    private List<GameObject> myList;

    void Awake() {
        myList = new List<GameObject>();
        myList.Add(grass1);
        myList.Add(grass2);
        myList.Add(flower1);
        myList.Add(flower2);
    }
    

    void Start () {
	    for(int n=0; n<20; n++) {
            float i = Random.Range(-10, 30);
            float j = Random.Range(-7, 7);
            int element = (int)Random.Range(0, myList.Count-1);
            GameObject newElement = (GameObject) Instantiate(myList[element]);
            newElement.transform.position = new Vector3(i, j, 0);
        }
	}
	

	void Update () {
	
	}
}
