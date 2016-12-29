using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class newgame : MonoBehaviour {
	public Button Button;

	void Start () {
		Button btn = Button.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);



		//Application.LoadLevel(levelToLoad);
	}

	void TaskOnClick(){
	//	Debug.Log ("You have clicked the button!");
		SceneManager.LoadScene("myProject");
	}
}