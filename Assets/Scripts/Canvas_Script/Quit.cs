using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Quit : MonoBehaviour {
	public Button Button;

	void Start () {
		Button btn = Button.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
			
	}
	
	void TaskOnClick(){
		Debug.Log ("You have clicked the button!");

		Application.Quit();

	}
}	