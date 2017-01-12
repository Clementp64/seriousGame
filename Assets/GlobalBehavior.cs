using UnityEngine;
using System.Collections;

public class GlobalBehavior : MonoBehaviour {
	public float maxHpPlayer;
	public float damagePlayer;
	public float moveSpeedPlayer;
	public int scorePlayer;

	public int nbUpgradeHp;
	public int nbUpgradeSpeed;
	public int nbUpgradeDamage;
	public int priceUpgradeHp;
	public int priceUpgradeSpeed;
	public int priceUpgradeDamage;
    
	void Start () {
	}

	void Awake() {
		initValue ();

		DontDestroyOnLoad (this);
		DontDestroyOnLoad (Database.Information);
	}

	public void initValue(){
		maxHpPlayer = 3;
		damagePlayer = 3;
		moveSpeedPlayer = 6;
		scorePlayer = 0;

		nbUpgradeHp = 0;
		nbUpgradeSpeed = 0;
		nbUpgradeDamage = 0;
		priceUpgradeHp = 5;
		priceUpgradeSpeed = 5;
		priceUpgradeDamage = 5;

		Database.Information.Initialize();
	}
}
