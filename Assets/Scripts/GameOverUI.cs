using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour {

    Button firstButton;
    string score;
    Text scoreText;

    void Start() {
        firstButton = GetComponentInChildren<Button>();
        firstButton.Select();
        if(GameObject.FindGameObjectWithTag("Player") != null) {
            float temp = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehavior>().GetScore() * 100;
            if (temp > 95)
                score = "S";
            else if (temp > 85)
                score = "A";
            else if (temp > 75)
                score = "B";
            else if (temp > 65)
                score = "C";
            else
                score = "D";
            transform.Find("scoreText").GetComponent<Text>().text = "Score : " + score;
        } else {
            transform.Find("scoreText").GetComponent<Text>().text = "Les déchets ont gagné";
        }
    }

    public void SetTitle(string t) {
        transform.Find("Title").GetComponent<Text>().text = t;
    }

	// Use this for initialization
	public void Quit()
	{
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<FMODUnity.StudioEventEmitter> ().Stop ();
		Debug.Log ("Application Quit !");
		//Application.Quit ();
		SceneManager.LoadScene("menu");

	}

	public void Retry ()
	{
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<FMODUnity.StudioEventEmitter> ().Stop ();
		Debug.Log ("Application Retry !");
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

}
