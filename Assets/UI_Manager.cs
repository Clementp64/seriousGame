using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UI_Manager : MonoBehaviour {


    public GameObject panelOptions;
    public GameObject cloud1,cloud2;
    public GameObject Canvas;
    public Sprite noVolume;
    public Sprite midVolume;
    public Sprite fullVolume;
    Button firstButton;
    Image volumeHandleImage;
    public GameObject volumeHandle;
    float volume;

    // Use this for initialization
    void Awake () {
        firstButton = Canvas.GetComponentInChildren<Button>();
        StartCoroutine(SelectFirstButton());
        //Réglage du volume
        volumeHandleImage = volumeHandle.GetComponent<Image>();
        volume = PlayerPrefs.GetFloat("Volume");
        SetVolume(volume);
    }

    IEnumerator SelectFirstButton() {
        EventSystem.current.SetSelectedGameObject(null, new BaseEventData(EventSystem.current));
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(firstButton.gameObject, new BaseEventData(EventSystem.current));

    }

    // Update is called once per frame
    void Update () {

        //--- Animation nuages---
        cloud1.transform.Translate(Vector3.left * 0.008f);
        cloud2.transform.Translate(Vector3.left * 0.005f);
        if(cloud1.transform.position.x < -12f)
        {
            cloud1.transform.position = new Vector3(12f, cloud1.transform.position.y, cloud2.transform.position.z);
        }
        if (cloud2.transform.position.x < -12f)
        {
            cloud2.transform.position = new Vector3(12f, cloud2.transform.position.y, cloud2.transform.position.z);
        }
    }

   public void StartNewGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ShowOptions()
    {
        panelOptions.SetActive(!panelOptions.activeInHierarchy);
        //Volume Slider 
        if (panelOptions.activeInHierarchy) {
        GameObject.FindGameObjectWithTag("SliderVolume").GetComponent<Slider>().value = volume;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game exited ! Bye bye !");
    }

    //---Options---
    public void ChangeVolume(GameObject myObject)
    {
        Slider mySlider = myObject.GetComponent<Slider>();
        SetVolume(mySlider.value);
        UpdateVolumeSprite(mySlider);
    }

    public void SetVolume(float vol)
    {
        PlayerPrefs.SetFloat("Volume", vol);
        GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>().volume = 0.1f * vol / 3.0f;
        volume = PlayerPrefs.GetFloat("Volume");
    }

    void UpdateVolumeSprite(Slider mySlider)
    {
        if (mySlider.value == 0)
            volumeHandleImage.sprite = noVolume;
        else if (mySlider.value < 4)
            volumeHandleImage.sprite = midVolume;
        else
            volumeHandleImage.sprite = fullVolume;
    }

}
