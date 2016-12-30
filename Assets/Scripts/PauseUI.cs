using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;

public class PauseUI : MonoBehaviour {

    Button firstButton;
    public Sprite noVolume;
    public Sprite midVolume;
    public Sprite fullVolume;
    public GameObject volumeHandle;
    Image volumeHandleImage;
    float volume;

    void Awake() {
        firstButton = GetComponentInChildren<Button>();
        volumeHandleImage = volumeHandle.GetComponent<Image>();
        //volume 
        volume = PlayerPrefs.GetFloat("Volume");
        GameObject.FindGameObjectWithTag("SliderVolume").GetComponent<Slider>().value = volume;//Changer le slider
    }

    public void Select() {
        //EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
    }

    public void Deselect() {
        EventSystem.current.SetSelectedGameObject(null);
        gameObject.SetActive(false);
    }

    // Use this for initialization
    public void Quit(){
		Debug.Log ("Application Quit !");
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<FMODUnity.StudioEventEmitter> ().Stop ();
		//Application.Quit ();
		SceneManager.LoadScene("menu");

	}

	public void Retry (){
		
		Debug.Log ("Application Retry !");
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<FMODUnity.StudioEventEmitter> ().Stop ();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

    public void Resume() {
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehavior>().ResumeGame();
    }

    public void ChangeVolume(GameObject myObject) {
        Slider mySlider = myObject.GetComponent<Slider>();
        SetVolume(mySlider.value);
        UpdateVolumeSprite(mySlider);
    }

    public void SetVolume(float vol)
    {
        PlayerPrefs.SetFloat("Volume", vol);
        GameObject.FindGameObjectWithTag("Global").GetComponent<AudioSource>().volume = 0.1f * vol / 3.0f;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FMODUnity.StudioEventEmitter>().SetParameter("volumeMusique", vol / 6f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehavior>().SetVolume(vol / 3.0f);
    }

    void UpdateVolumeSprite(Slider mySlider) {
        if (mySlider.value == 0)
            volumeHandleImage.sprite = noVolume;
        else if (mySlider.value < 4)
            volumeHandleImage.sprite = midVolume;
        else
            volumeHandleImage.sprite = fullVolume;
    }
}
