using UnityEngine;
using System.Collections;

public class CharacterBehavior : MonoBehaviour {

    public GameObject PlayerHpHandler;
    public float moveSpeed = 4;
    public Transform weaponParent;
    public Transform shieldParent;
    public Transform previewItemParent;
    private bool isMoving;
    Animator anim;
    private static float movementX;
    private static float movementY;
    public Vector3 vectMovement = new Vector3(0,0,0);
    Rigidbody2D myBody;
    private CharacterInteractionModel InteractionModel;
    bool isFrozen = false;
    bool isAttacking = false;
    public ItemType equipedWeapon = ItemType.None;
    private float pushTime;
    private Vector3 pushDirection;
    private InteractablePickup carriedObject = null;
    private string layerOfCarriedObject;
    private float maxHp = 3;
    private float currentHp;
    private bool isImmune = false;
    public float damage = 3;
    float nbErrors = 0;
    float nbEnemies = 0;
    bool isPaused = false;
    bool pauseFrozen;
    public GameObject pausePannel;


    AudioSource audio;
    Object[] attackSounds;
    Object[] pointsSound;
	Object[] shootSounds;
    Object hurtSound;
	Object heartSound;

    void Start() {
        currentHp = maxHp;
        UpdateDisplayHearts();
        anim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        InteractionModel = GetComponent<CharacterInteractionModel>();
        HideWeapon();
        audio = gameObject.AddComponent<AudioSource>();
        audio.playOnAwake = false;
        audio.volume = 0.8f;
        attackSounds = Resources.LoadAll("Sounds/AttacksBis");
        pointsSound = Resources.LoadAll("Sounds/Coins");
        hurtSound = Resources.Load("Sounds/Hurt");
		shootSounds = Resources.LoadAll ("Sounds/Shoot");
		heartSound = Resources.Load ("Sounds/heart");
        setFrozen(false, true);
    }
    
    void Update() {
        UpdateAttack();
        UpdateAction();
        UpdateMovement();
    }

    void UpdateAttack() {
        if (Input.GetKeyDown(KeyCode.C))
            if (CanAttack()) {
                anim.SetTrigger("DoAttack");
				audio.clip = (AudioClip) attackSounds[Random.Range(0, attackSounds.Length)];
				audio.Play();
            }
    }

    void UpdateAction() {
        if (Input.GetKeyDown(KeyCode.Space))
            OnActionPressed();
        else if (QuestionBox.IsVisible()) {
            if (Input.GetKeyDown(KeyCode.DownArrow))
                QuestionBox.LowerSelection();

            if (Input.GetKeyDown(KeyCode.UpArrow))
                QuestionBox.GoUpSelection();
        }
        else if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                ResumeGame();
            } else {
				GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<FMODUnity.StudioEventEmitter> ().SetParameter ("paused", 1f);
                isPaused = true;
                pausePannel.SetActive(true);
                pausePannel.GetComponent<PauseUI>().Select();
                pauseFrozen = isFrozen;
                setFrozen(true, true);
            }
        }
    }

    public void ResumeGame() {
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<FMODUnity.StudioEventEmitter> ().SetParameter ("paused", 0f);
        isPaused = false;
        pausePannel.GetComponent<PauseUI>().Deselect();
        setFrozen(pauseFrozen, true);
    }

    void UpdateMovement() {

        isMoving = false;
        movementX = 0;
        movementY = 0;

        if (isFrozen || isAttacking) {
            myBody.velocity = new Vector3(0, 0, 0);
        } else if (IsBeingPushed()) {
            myBody.velocity = pushDirection;
            pushTime = Mathf.MoveTowards(pushTime, 0f, Time.deltaTime);
            anim.SetBool("IsHit", IsBeingPushed());
        } else {
            if (Input.GetKey(KeyCode.LeftArrow)) {
                movementX = -1;
                isMoving = true;
            } else if (Input.GetKey(KeyCode.RightArrow)) {
                movementX = 1;
                isMoving = true;
            }
            if (Input.GetKey(KeyCode.UpArrow)) {
                movementY = 1;
                isMoving = true;
            } else if (Input.GetKey(KeyCode.DownArrow)) {
                movementY = -1;
                isMoving = true;
            }
            if (isMoving) {
                vectMovement.x = movementX;
                vectMovement.y = movementY;
                vectMovement.Normalize();
                anim.SetFloat("MovementX", vectMovement.x);
                anim.SetFloat("MovementY", vectMovement.y);
                myBody.velocity = moveSpeed * vectMovement;
            } else {
                myBody.velocity = new Vector3(0, 0, 0);
            }
        }
        anim.SetBool("IsMoving", isMoving && !isFrozen);
    }

    //freeze/unfreeze the character
    public void setFrozen(bool frozen, bool freezeTime) {
        isFrozen = frozen;
        if (freezeTime) {
            if (isFrozen)
                StartCoroutine(FreezeTimeRoutine());
            else
                Time.timeScale = 1;
        }
    }
    //wait end of frame before freezing the time for the animation
    IEnumerator FreezeTimeRoutine() {
        yield return new WaitForEndOfFrame();
        Time.timeScale = 0;
    }

    public Vector3 GetDirection() {
        return vectMovement;
    }

    //when player press interaction key
    void OnActionPressed() {
        if (IsCarrying()) {
            DropObject();
            return;
        }
        if(InteractionModel != null)
            InteractionModel.OnInteract();
    }

    //returns true if the character can attack
    bool CanAttack() {
        if (isAttacking) return false;  //already attacking
        if (equipedWeapon == ItemType.None) return false;   //no weapon equiped
        if (IsBeingPushed()) return false;  //being pushed
        if (IsCarrying()) return false;     //carrying smth
        return true;
    }

    public void EquipWeapon(ItemType weapon) {
        if (weaponParent == null) return;
        ItemData itemData = Database.Items.FindItem(weapon);
        if (itemData == null) return;
        if (itemData.Equip == ItemData.EquipType.NotEquipable) return;

        equipedWeapon = weapon;

        //create the prefab of the weapon in WeaponParent
        GameObject newEquipableObject = (GameObject)Instantiate(itemData.Prefab);
        newEquipableObject.transform.parent = weaponParent;
        newEquipableObject.transform.localPosition = Vector2.zero;
        newEquipableObject.transform.localRotation = Quaternion.identity;
    }

    public void PreviewItem(ItemType item) {
        ItemData itemData = Database.Items.FindItem(item);
        if (itemData == null) return;
        if (itemData.Equip == ItemData.EquipType.NotEquipable) return;

        GameObject newEquipableObject = (GameObject)Instantiate(itemData.Prefab);
        newEquipableObject.transform.parent = previewItemParent;
        newEquipableObject.transform.localPosition = Vector2.zero;
        newEquipableObject.transform.localRotation = Quaternion.identity;

        if (itemData.Animation == ItemData.PickupAnimation.OneHanded)
            StartPickUp1Animation();
        else if (itemData.Animation == ItemData.PickupAnimation.TwoHanded)
            StartPickUp2Animation();
    }

    //player can't move when pickup animation is being played
    public void OnAnimationStarted() {
        setFrozen(true, false);
    }

    public void OnAnimationFinished() {
        setFrozen(false, false);
    }

    public void OnAttackStarted() {
        isAttacking = true;
    }

    public void OnAttackFinished() {
        isAttacking = false;
    }

    public void ShowWeapon() {
        if (weaponParent != null)
            StartCoroutine(ShowWeaponAfterDelay());
    }

    IEnumerator ShowWeaponAfterDelay() {
        yield return new WaitForSeconds(0.01f);
            weaponParent.gameObject.SetActive(true);

    }

    public void HideWeapon() {
        if (weaponParent != null)
            weaponParent.gameObject.SetActive(false);
    }

    public void HidePreviewItem() {
        foreach (Transform child in previewItemParent.transform)
            GameObject.Destroy(child.gameObject);
    }

    //draw weapon behind character
    public void SetWeaponBehind() {
        if (weaponParent == null) return;
        weaponParent.GetComponentInChildren<SpriteRenderer>().sortingOrder = 90;
    }

    //draw weapon above character
    public void SetWeaponAbove() {
        if (weaponParent == null) return;
        weaponParent.GetComponentInChildren<SpriteRenderer>().sortingOrder = 110;
    }

    public void StartPickUp1Animation() {
        anim.SetTrigger("DoPickUp1");
    }
    
    public void StartPickUp2Animation() {
        anim.SetTrigger("DoPickUp2");
    }

    //character is pushed back in a direction
    public void PushBack(Vector3 pushVect, float time) {
        if (isImmune) return;
        pushTime = time;
        pushDirection = pushVect;
        OnAttackFinished();         //end attack
        setFrozen(false, false);    //freeze player
        if (IsCarrying())
            DropObject();
        HidePreviewItem();
        HideWeapon();
        anim.SetBool("IsHit", true);
    }

    public void LoseHp(float i) {
        if (isImmune) return;
        audio.clip = (AudioClip) hurtSound;
        audio.Play();
        StartCoroutine(ManageImmunity(1.5f));
        currentHp -= i;
        UpdateDisplayHearts();
	
		if (currentHp <= 0) {
			Debug.Log ("dead");
			GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<FMODUnity.StudioEventEmitter> ().Stop ();
			gameObject.SetActive(false);
			EndGame ();
		}

		if(currentHp <= 1)
			GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<FMODUnity.StudioEventEmitter> ().SetParameter ("warning", 1);
		
		
    }
						//  Game Over
	[SerializeField]
	private GameObject gameOverUI;

	public void EndGame(){
		gameOverUI.SetActive(true);
        gameOverUI.GetComponent<GameOverUI>().SetTitle("Game Over !");
    }
		

    IEnumerator ManageImmunity(float delay) {
        StartCoroutine(BlinkRenderer(delay));
        isImmune = true;
        yield return new WaitForSeconds(delay);
        isImmune = false;
    }

    IEnumerator BlinkRenderer(float duration) {
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.15f);
        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.15f);
        duration -= 0.3f;
        if (duration >= 0.15)
            StartCoroutine(BlinkRenderer(duration));

    }

    void UpdateDisplayHearts() {
        PlayerHpHandler.GetComponent<PlayerHpBehavior>().UpdateDisplay(maxHp, currentHp);
    }

    //returns true if player is being pushed right now
    public bool IsBeingPushed() {
        return (pushTime > 0);
    }

    //Pickup and carry an InteractablePickup item
    public void CarryObject(InteractablePickup objectToCarry) {
        if (IsCarrying()) return;
        carriedObject = objectToCarry;
        anim.SetTrigger("DoPickupObject");  //pickup animation
        anim.SetBool("IsPickingUp", true);  //special Idle and Walk animation when carrying
        carriedObject.transform.parent = previewItemParent; //object becomes child of previewItemParent
        carriedObject.transform.localPosition = Vector3.zero;
        layerOfCarriedObject = carriedObject.GetComponentInChildren<SpriteRenderer>().sortingLayerName;
        carriedObject.GetComponentInChildren<SpriteRenderer>().sortingLayerName = "HighObjects";    //rendered above the character
        SetColliders(carriedObject.gameObject, false);  //disable all coliders of the carried object
        EnemyBehavior objectBehavior = carriedObject.GetComponent<EnemyBehavior>();
        if (objectBehavior != null)
            objectBehavior.DisableShadow();
        TipsHandler.Recycle();
    }

    public void DropObject() {
        if (!IsCarrying()) return;

		// Vérification qu'il n'y a pas d'obstacle devant
		Collider2D[] closeColliders = Physics2D.OverlapCircleAll(transform.position + (vectMovement * 0.7f), 0.55f);
		foreach (Collider2D closeCollider in closeColliders) {
			if (closeCollider.CompareTag ("Obstacle")) {
				// L'objet est déposé derrière
				vectMovement = -vectMovement;
				break;
			}
		}

        anim.SetTrigger("DoDrop");  //drop animation
        Vector3 newPosition = vectMovement;
        newPosition.y -= previewItemParent.transform.localPosition.y + 0.2f;
        carriedObject.transform.localPosition = newPosition;    //move it in front of character
        carriedObject.transform.parent = null;  //no parent
        carriedObject.GetComponentInChildren<SpriteRenderer>().sortingLayerName = layerOfCarriedObject;
        if(carriedObject.GetComponentInChildren<SpriteRenderer>().sortingOrder < 200)
            carriedObject.GetComponentInChildren<SpriteRenderer>().sortingOrder += 100;
        SetColliders(carriedObject.gameObject, true); EnemyBehavior objectBehavior = carriedObject.GetComponent<EnemyBehavior>();
        if (objectBehavior != null)
            objectBehavior.EnableShadow();
        carriedObject = null;
        anim.SetBool("IsPickingUp", false);     //go back to normal animations
    }

    //true if player is carrying smth
    public bool IsCarrying() {
        return (carriedObject != null);
    }

    //enable/disable all colliders and rigidbody of an object and its children
    void SetColliders(GameObject obj, bool b) {
        if (obj.GetComponent<Rigidbody2D>() != null)
            obj.GetComponent<Rigidbody2D>().isKinematic = !b;

        if (obj.GetComponent<Collider2D>() != null)
            obj.GetComponent<Collider2D>().enabled = b;

        foreach(Collider2D collider in obj.GetComponentsInChildren<Collider2D>())
            collider.enabled = b;
    }

    //returns the carried object or null
    public GameObject GetCarriedObject() {
        if (carriedObject == null) return null;
        return carriedObject.gameObject;
    }

	public void GainPoints(int nb) {
        CharacterInventoryModel inventory = GetComponent<CharacterInventoryModel>();
        if (inventory == null) return;
        inventory.AddItem(ItemType.RecyclingPoints, nb);
        TipsHandler.RecyclingPoints();
        audio.clip = (AudioClip) pointsSound[Random.Range(0, pointsSound.Length)];
        audio.Play();
    }

	public void LosePoints(int nb) {

    }

    public void UpgradeSpeed() {
        moveSpeed += 0.75f;
    }

    public void UpgradeHealth() {
        maxHp++;
        currentHp++;
        UpdateDisplayHearts();
    }

    public void UpgradeDamage() {
        damage++;
    }

    public void AddError() {
        nbErrors++;
    }

    public void AddEnemy() {
        nbEnemies++;
    }

    public float GetScore() {
        if (nbEnemies == 0)
            return 0;
        return (1f - (nbErrors/nbEnemies));
    }

	public void gainHp(){
		currentHp += 1f;
		if(currentHp > 1f)
			GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<FMODUnity.StudioEventEmitter> ().SetParameter ("warning", 0);
		if (currentHp > maxHp)
			currentHp = maxHp;
		UpdateDisplayHearts();
		audio.clip = (AudioClip) heartSound;
		audio.Play();
	}
    
    public void SetVolume(float vol) {
        audio.volume = 0.8f * vol;
    }

    public void PlaySound(AudioClip clip) {
        audio.clip = clip;
        audio.Play();
    }

	public AudioClip soundShoot () {
		return (AudioClip)shootSounds [Random.Range (0, shootSounds.Length)];
	}
}