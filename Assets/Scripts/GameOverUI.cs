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
			transform.FindChild ("NextButton").gameObject.SetActive (true);

			CharacterBehavior player = GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterBehavior> ();
			float temp = player.GetScore() * 100;
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

			GlobalBehavior global = GameObject.FindGameObjectWithTag ("Global").GetComponent<GlobalBehavior> ();
			global.maxHpPlayer = player.getMaxHp ();
			global.damagePlayer = player.getDamage ();
			global.moveSpeedPlayer = player.getMoveSpeed ();
			global.scorePlayer = player.getRecyclePoint ();

			global.nbUpgradeHp = Upgrades.Instance.levels [0];
			global.nbUpgradeSpeed = Upgrades.Instance.levels [1];
			global.nbUpgradeDamage = Upgrades.Instance.levels [2];
			global.priceUpgradeHp = Upgrades.Instance.prices [0];
			global.priceUpgradeSpeed = Upgrades.Instance.prices [1];
			global.priceUpgradeDamage = Upgrades.Instance.prices [2];

        } else {
			transform.FindChild ("NextButton").gameObject.SetActive (false);
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

	public void Next2 ()
	{
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<FMODUnity.StudioEventEmitter> ().Stop ();
		Debug.Log ("Level 2 !");
		SceneManager.LoadScene("Level2");
	}

}
