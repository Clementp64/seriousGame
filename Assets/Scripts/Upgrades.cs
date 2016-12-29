using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Upgrades : MonoBehaviour {

    static Upgrades Instance;
    public GameObject buttons;
    Image myImage;
    CharacterInventoryModel inventory;
    int[] prices;
    int[] levels;
    UpgradeButtonBehavior selectedButton;
    public Sprite[] levelsSprites;
    CharacterBehavior playerBehavior;
    AudioSource audio;

    void Awake() {
        
        myImage = GetComponent<Image>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterInventoryModel>();
        playerBehavior = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehavior>();
        prices = new int[] { 5, 5, 5};
        levels = new int[] { 0, 0, 0 };
        Instance = this;
        audio = gameObject.AddComponent<AudioSource>();
        audio.playOnAwake = false;
        audio.clip = (AudioClip)Resources.Load("Sounds/Buttons/woodenButton");
    }

    void Start() {
        UpdatePrices();
        UpdateLevels();
    }

    public static void Show() {
        Instance.InstanceShow();
    }

    void InstanceShow() {
        myImage.enabled = true;
        buttons.SetActive(true);
        buttons.GetComponentInChildren<Button>().Select();


    }

    public static void Hide() {
        Instance.InstanceHide();
    }

    public void InstanceHide() {
        playerBehavior.setFrozen(false, true);
        StartCoroutine(HideElements());

    }

    public static bool IsShowing() {
        return Instance.InstanceIsShowing();
    }

    IEnumerator HideElements() {
        yield return new WaitForSeconds(0);
        myImage.enabled = false;
        buttons.SetActive(false);
    }

    bool InstanceIsShowing() {
        return myImage.enabled;
    }

    public void ButtonPressed(int i) {
        if (levels[i] == 5) return;
        if (inventory.GetItemCount(ItemType.RecyclingPoints) < prices[i]) return;
        audio.pitch = 1 + (levels[i] * 0.1f);
        audio.Play();
        inventory.AddItem(ItemType.RecyclingPoints, -prices[i]);
		prices[i] += (2 + (levels[i] * levels[i]));
        levels[i]++;
        if(levels[i] == 5) {

        }
        UpdatePrices();
        UpdateLevels();
        InstanceButtonSelected(selectedButton);

        switch (selectedButton.index) {
            case 0:
                playerBehavior.UpgradeHealth();
                break;
            case 1:
                playerBehavior.UpgradeSpeed();
                break;
            case 2:
                playerBehavior.UpgradeDamage();
                break;
            default:
                Debug.Log("Not implemented");
                break;
        }
    }

    public static void ButtonSelected(UpgradeButtonBehavior behavior) {
        Instance.InstanceButtonSelected(behavior);
    }

    void InstanceButtonSelected(UpgradeButtonBehavior behavior) {
        selectedButton = behavior;
        if (prices[behavior.index] > inventory.GetItemCount(ItemType.RecyclingPoints))
            behavior.SetColor(Color.red);
        else
            behavior.SetColor(Color.green);
    }

    void UpdatePrices() {
        int nbUpgrades = transform.GetChild(0).childCount - 1;
        for (int i = 0; i < nbUpgrades; i++) {
            if(levels[i] < 5)
                transform.GetChild(0).GetChild(i).Find("Cost").GetComponentInChildren<Text>().text = prices[i].ToString("000");
            else {
                transform.GetChild(0).GetChild(i).Find("Cost").GetComponentInChildren<Text>().text = "";
                transform.GetChild(0).GetChild(i).Find("Cost").GetComponentInChildren<Image>().enabled = false;
            }

        }
    }

    void UpdateLevels() {
        int nbUpgrades = transform.GetChild(0).childCount - 1;
        for (int i = 0; i < nbUpgrades; i++) {
            transform.GetChild(0).GetChild(i).Find("Level").GetComponent<Image>().sprite = levelsSprites[levels[i]];

        }
    }
}
