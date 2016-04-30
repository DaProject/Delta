using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerManagerBackup : MonoBehaviour
{

	// States of the player

	public enum PlayerStates {AWAKE, IDLE, ATTACK_10, ATTACK_01, SWORD_10, SWORD_20, CHAIN_01, CHAIN_02, DASH, DAMAGED, STUNNED, DEAD, VICTORY}

	[Header("States")]
	public PlayerStates state;

	// Health
	[Header("Health")]
	public int maxHealth;
	public int currentHealth;

    // Controller
    [Header("Controller")]
    public float playerSpeed;                       // The speed of the player.
    public float playerRotation;
    public float moveHorizontal;                    // Variable that gets the horizontal axis value.
    public float moveVertical;                      // Variable that gets the vertical axis value.
    private Vector3 movement;                       // Vector 3 with the values of the movement.

    // Movement
    [Header("Movement")]
    public float turnSmoothing;                     // A smoothing value for turning the player.
    public float speedDampTime;                     // The damping for the speed parameter.

    // Damage
    [Header("Attacks & Habilities")]
    public int attackDamage;                        // Auxiliar variable that gets the value of the differents attacks. After, is used to apply the damage to the enemy.
    public int attack10;                            // Variable with the damage of the attack10 (sword).
    public int sword10;                             // Variable with the damage of the sword10 (sword hability).
    public int sword20;                             // Variable with the damage of the sword20 (sword hability).
    public int attack01;                            // Variable with the damage of the attack01 (chain).
    public int chain01;                             // Variable with the damage of the chain01 (chain hability).
    public int chain02;                             // Variable with the damage of the chain02 (chain hability).
    public int speedAttack10;                       // The time the animation should last.
    public int speedSword10;                        // The time the animation should last.
    public int speedSword20;                        // The time the animation should last.
    public int speedAttack01;                       // The time the animation should last.
    public int speedChain01;                        // The time the animation should last.
    public int speedChain02;                        // The time the animation should last.
    public bool sword10Active;                      // Bool that allows to use the sword10 ability.
    //public bool sword20Active;                      // Bool that allows to use the sword20 ability.
    public bool chain10Active;                      // Bool that allows to use the chain10 ability.
    public bool trailActivation;

    //Stunned
    [Header("Stunned")]
    public float timeStunned;
    public float timeStunnedIni;

    // Dash
    [Header("Dash")]
    public float speedDash;                         // The speed wich the player makes a dash.
	public int maxDashResistance;
	public int currentDashResistance;
	public int resistancePerDash;
	public Slider DashResistanceSlider;

    //Sounds
    [Header("Sounds")]
    public AudioClip hurtClip;
	public AudioClip lowHpClip;
	public AudioClip deathClip;
	public AudioClip swordSwipeClip;
    AudioSource playerAudio;

	// UI Player
	[Header("UI")]
	public Slider healthSlider;                     // It shows the health bar.
	public Image damageImage;                       // The UI image that shows when the player gets hit.
    public Image swordSprite;
    public Image chainSprite;
    public Image sword10Sprite;
    public Image sword20Sprite;
    public Image chain10Sprite;
	public Color flashColor;                        // The color of the damageImage.

    public GameObject crackGround;

    // Timers
    [Header("Timers")]
	public float temp;
	public float tempDamage;                        // Counter that determinates how much time the player has to be in the DAMAGED state.
    public float tempAttack10;                      // Counter that reflects how much the animation of the attack10 longs.
    public float tempSword10;                       // Counter that reflects how much the animation of the sword10 longs.
    private float sword10CD;
    public float sword10CDAux;
    public float tempSword20;                       // Counter that reflects how much the animation of the sword20 longs.
    public float tempAttack01;                      // Counter that reflects how much the animation of the attack01 longs.
    public float tempChain01;                       // Counter that reflects how much the animation of the chain10 longs.
    public float tempChain02;                       // Counter that reflects how much the animation of the chain02 longs.
    public float tempSlash;                         // Counter that reflects how much the animation of the slash longs.
    public float tempDash;                          // Counter that determinates how much time the player has to be in the DASH state.
    public float tempSword10Stun;                   // Counter that determinates when the sword10 attack is going to stun the enemies.
    public float tempSword10StunAux;
    public float attackStateCounter;                // Auxiliar variable that says how much time the player has to be in the ATTACKXX state. It gets the value from the different counters of each attack.

    // Control player
    [Header("Control player")]
    public GameObject attack01Gameobject;           // Gets the Attack01 gameobject from the Attack01 gameobjects child's player.
    public GameObject sword;
    public Material swordMaterial;                  // Material from the sword. TODO: make it work.
    public BoxCollider attackAction;                // Gets the attackAction collider from the AttackAction child's player.
    public SphereCollider sphereCollider;           // Gets the player spherecollider.
    public SphereCollider attack01Collider;		    // Gets the attack01Action collider from the attack01Collider child's player.
    public SphereCollider sword10Collider;          // Gets the player sword10Collider.
    public CapsuleCollider capsuleCollider;         // Gets the player capsulecollider.
    public bool godMode;
    public float attack01ColliderRadius;			// Auxiliar variable that sets the radius of the attack01Collider
    public Rigidbody rigidBody;                    // The rigidbody from the player.
    public CameraShake cameraShake;
    public GameObject pointsText;
    public PointCounter score;
    public Text newAbility;
    
    public bool fadeAlpha = false;
    public float fadeCounter = 0.0f;
    public float alphaValue;
    private float sword10ColorRValue;
    public float sword10ColorRValueAux;
    private float sword10ColorGValue;
    public float sword10ColorGValueAux;
    private float sword10ColorBValue;
    public float sword10ColorBValueAux;

    //Lost&Victory
    [Header("Lost&Victory")]
    public Image blackScreen;
    public float counterMenu;
    public float counterMenuAux;
    public Text victoryText;
    public Text lostText;
    public Text cooldownSword10;


    public ParticleSystem slashFX;

    // Animations
    Animator anim;                                  // The animator component from the player.

	// Use this for initialization
	void Start ()
    {
		setAwake ();                                // Call the setAwake function.
    }
	
	// Update is called once per frame
    void FixedUpdate()
    {
        switch(state)
        {
            case PlayerStates.IDLE:
                IdleBehaviour();
                break;
            case PlayerStates.DASH:
                DashBehaviour();
                break;
        }
    }

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.G)) godMode = !godMode;        // Changes between godmode and normal mode whenever the G key is pressed. G for God
        if (godMode)
        {
            sphereCollider.enabled = false;                         // Sets the sphereCollider from the player to false.
            capsuleCollider.enabled = false;                        // Sets the capsuleCollider from the player to false.
            rigidBody.useGravity = false;                           // Deactivates the gravity from the player. Can no longer falls.
        }
        else
        {
            sphereCollider.enabled = true;                          // Sets the sphereCollider from the player to true.
            capsuleCollider.enabled = true;                         // Sets the capsuleCollider from the player to true.
            rigidBody.useGravity = true;                            // Activates the gravity from the player. Can fall.
        }

		switch (state)
        {
			case PlayerStates.AWAKE:
				AwakeBehaviour();
				break;
            case PlayerStates.ATTACK_10:
                Attack10Behaviour();
                break;
            case PlayerStates.ATTACK_01:
                Attack01Behaviour();
                break;
            case PlayerStates.SWORD_10:
                Sword10Behaviour();
                break;
            case PlayerStates.SWORD_20:
                Sword20Behaviour();
                break;
            case PlayerStates.CHAIN_01:
                Chain01Behaviour();
                break;
            case PlayerStates.CHAIN_02:
                Chain02Behaviour();
                break;
            case PlayerStates.DAMAGED:
				DamagedBehaviour();
				break;
            case PlayerStates.STUNNED:
                StunnedBehaviour();
                break;
            case PlayerStates.DEAD:
				DeadBehaviour();
				break;
			case PlayerStates.VICTORY:
				VictoryBehaviour();
				break;
		}

		if (DashResistanceSlider.value < 100) DashResistanceSlider.value ++;        // Start gaining value when the current DashResistanceSlider.value falls from 100.
		if (currentHealth <= 10) 
		{
			playerAudio.clip = lowHpClip;
			playerAudio.Play();  
		}

        if (score.pointsCounter >= 100)
        {
            sword10Active = true;
            sword10Sprite.enabled = true;
            if(alphaValue >= 0) newAbility.color = new Color(1.0f, 1.0f, 1.0f, alphaValue -= 0.2f * Time.deltaTime);
        }

        if (sword10CD >= 0)
        {
            sword10CD -= 1 * Time.deltaTime;
            cooldownSword10.enabled = true;
            cooldownSword10.text = "" + sword10CD;
            sword10Sprite.color = new Color(1.0f, sword10ColorGValue += 1 / sword10CDAux * Time.deltaTime, sword10ColorBValue += 1 / sword10CDAux * Time.deltaTime);
        }
        else if (sword10CD <= 0)
        {
            cooldownSword10.enabled = false;
            sword10Sprite.color = new Color(1.0f, 1.0f, 1.0f);
        }
    }

	// Behaviours
	private void AwakeBehaviour()
	{
		setIdle ();                                                                 // Initalization.
	}

	private void IdleBehaviour()
    {
        trailActivation = false;

        if (Input.GetKey(KeyCode.W))
        {
            moveForward(playerSpeed);
            anim.SetBool("IsWalking", true);
        }
        else if (Input.GetKeyUp(KeyCode.W)) anim.SetBool("IsWalking", false);
        if (Input.GetKey(KeyCode.S))
        {
            moveBackward(playerSpeed);
            anim.SetBool("IsWalking", true);
        }

        else if (Input.GetKeyUp(KeyCode.S)) anim.SetBool("IsWalking", false);
        if (Input.GetKey(KeyCode.D)) rotateRight();
        if (Input.GetKey(KeyCode.A)) rotateLeft();

        if (currentHealth <= 0) setDead();

        if (Input.GetMouseButtonDown(0)) setAttack10();                                                                 // Calls the setAttack10 function if mouse left button is pressed.
        else if (Input.GetMouseButtonDown(1)) setAttack01();                                                            // Calls the setAttack01 function if mouse right button is pressed.

		if (Input.GetKeyDown(KeyCode.Alpha1) && sword10Active && sword10CD <= 0) setSword10();                                            // Calls the setSword10 function if the 1 keypad is pressed, and if is not in chain mode.
        else if (Input.GetKeyDown(KeyCode.Alpha2) && chain10Active) setChain01();                                       // If the chain mode is active, goes to the setChain01.

        if (Input.GetKeyDown(KeyCode.LeftShift)  && (DashResistanceSlider.value >= resistancePerDash)) setDash();       // Calls the setDash function if left shift key is pressed.

        
    }

    private void Attack10Behaviour()
    {
        cameraShake.ShakeFunction();

        trailActivation = true;

        attackStateCounter -= Time.deltaTime;           // Starts the countdown after the attack has been done.

        if (attackStateCounter <= 0) setIdle();         // Goes back to setIdle if the player has not attack for a small amount of time.

        if (!slashFX.isPlaying)
        {
            slashFX.Play(true);
        }
    }
    
    private void Attack01Behaviour()
    {
        cameraShake.ShakeFunction();

        attackStateCounter -= Time.deltaTime;

        attack01Collider.radius += 10 * Time.deltaTime;        // Makes the attack01Collider get bigger during the attack01Behaviour.


        if (attackStateCounter <= 0) setIdle();


    }
    
    private void Sword10Behaviour()
    {
        attackStateCounter -= Time.deltaTime;

        Debug.Log("sword10Collider activated");
        sword10Collider.enabled = true;

        if (attackStateCounter <= 0) setIdle();
    }

    private void Sword20Behaviour()
    {
        attackStateCounter -= Time.deltaTime;

        if (attackStateCounter <= 0) setIdle();
    }
    
    private void Chain01Behaviour()
    {
        attackStateCounter -= Time.deltaTime;

        if (attackStateCounter <= 0) setIdle();
    }

    private void Chain02Behaviour()
    {
        attackStateCounter -= Time.deltaTime;

        if (attackStateCounter <= 0) setIdle();
    }
    
    private void DashBehaviour()
    {
        tempDash -= Time.deltaTime;

        sphereCollider.enabled = false;         // Sets the sphereCollider to false.
        capsuleCollider.enabled = false;        // Sets the capsuleCollider to false.
        rigidBody.useGravity = false;           // Deactivates the gravity from the player

        if (tempDash <= 0) setIdle();
    }

    private void DamagedBehaviour()
    {
        if (Input.GetKey(KeyCode.W))
        {
            moveForward(playerSpeed);
            anim.SetBool("IsWalking", true);
        }
        else if (Input.GetKeyUp(KeyCode.W)) anim.SetBool("IsWalking", false);
        if (Input.GetKey(KeyCode.S))
        {
            moveBackward(playerSpeed);
            anim.SetBool("IsWalking", true);
        }
        else if (Input.GetKeyUp(KeyCode.S)) anim.SetBool("IsWalking", false);
        if (Input.GetKey(KeyCode.D)) rotateRight();
        if (Input.GetKey(KeyCode.A)) rotateLeft();

        if (Input.GetMouseButtonDown(0)) setAttack10();                                                                 // Calls the setAttack10 function if mouse left button is pressed.
        else if (Input.GetMouseButtonDown(1)) setAttack01();                                                            // Calls the setAttack01 function if mouse right button is pressed.

		if (Input.GetKeyDown(KeyCode.Alpha1) && sword10Active) setSword10();                                               // Cals the setSword10 function if the 1 keypad is pressed, and if is not in chain mode.
        else if (Input.GetKeyDown(KeyCode.Alpha1) && chain10Active) setChain01();                                           // If the chain mode is active, goes to the setChain01.

        if (Input.GetKeyDown(KeyCode.LeftShift)  && (DashResistanceSlider.value >= resistancePerDash)) setDash();       // Calls the setDash function if left shift key is pressed.

        temp -= Time.deltaTime;

        damageImage.color = Color.Lerp(damageImage.color, Color.clear, tempDamage * Time.deltaTime);                    // Sets the difumination for the damageImage

        if (temp <= 0) setIdle();                                                            // If the player has not been attacked for a while, goes back to setIdle function
	}

    private void StunnedBehaviour()
    {
        timeStunned -= Time.deltaTime;

        if (timeStunned <= 0) setIdle();
    }

    private void DeadBehaviour()
	{
        //counterMenuAux = Time.realtimeSinceStartup * Time.deltaTime;
        // TODO: What happens when the player die.
        if (Time.realtimeSinceStartup - counterMenu <= 5)
        {
            blackScreen.enabled = true;
            lostText.enabled = true;
            Time.timeScale = 0.0f;
        }
        else Application.LoadLevel(0);
    }

	private void VictoryBehaviour()
	{
        //counterMenuAux = Time.realtimeSinceStartup;
        // TODO: WHat happens when the player wins.
        if (Time.realtimeSinceStartup - counterMenu <= 5)
        {
            blackScreen.enabled = true;
            victoryText.enabled = true;
            Time.timeScale = 0.0f;
        }
        else Application.LoadLevel(0);

        Debug.LogFormat("time.time{0} - counterMenu: {1}", Time.realtimeSinceStartup, counterMenu);
    }

	// Sets
	public void setAwake()
    { 
		currentDashResistance = maxDashResistance;

        currentHealth = maxHealth;                          		        // Sets the player health to the value of maxHealth that you indicated.

		sword10Active = false;                                		        // Sets the sword10Active bool to false by default.
        //sword20Active = false;                                		        // Sets the sword20Active bool to false by default.
        chain10Active = false;                                              // Sets the chain10Active bool to false by default.
        trailActivation = false;

        sphereCollider = GetComponent<SphereCollider>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        attackAction = GetComponentInChildren<BoxCollider>();      	        // Gets the BoxCollider of the PlaceHolder_Sword children.
		attack01ColliderRadius = attack01Collider.radius;

        crackGround.SetActive(false);

        playerAudio = GetComponent<AudioSource>();          		        // Gets the component AudioSource from the player.

        timeStunned = timeStunnedIni;

        flashColor = new Color(1.0f, 0.0f, 0.0f, 0.1f);           		    // Sets the color values for the damageImage.
        swordSprite.enabled = true;
        chainSprite.enabled = false;
        sword10Sprite.enabled = false;
        sword20Sprite.enabled = false;
        chain10Sprite.enabled = false;
        newAbility.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        
        pointsText = GameObject.FindGameObjectWithTag("Score");
        score = pointsText.GetComponent<PointCounter>();

        rigidBody = GetComponent<Rigidbody>();              		        // Gets the RigidBody from the GameObject.

        anim = GetComponent<Animator>();

        swordMaterial = GetComponent<Renderer>().material;

        blackScreen.enabled = false;
        //counterMenu = counterMenuAux;
        victoryText.enabled = false;
        lostText.enabled = false;
        cooldownSword10.enabled = false;

        state = PlayerStates.AWAKE;                         		        // Cals the AWAKE state.

        slashFX.Stop();
    }

	public void setIdle()
    {
        //Debug.Log("Idle");

		attackAction.enabled = false;                                                       // Deactivates the collider of the attack10 attack (sword).
		attack01Collider.enabled = false;                                                   // Deactivates the collider of the chain01 hability (chain).
        attack01Collider.radius = attack01ColliderRadius;                                   // Gives the chain01Collider it's previous radius value.
        sword10Collider.enabled = false;

        rigidBody.isKinematic = false;                                                      // Deactivates the isKinematic bool of the rigidbody.

        tempDash = 0.5f;                                                                    // Resets the tempDash counter.
        temp = 0.0f;

        damageImage.enabled = false;                                                        // Deactivation of the damageImage.

        state = PlayerStates.IDLE;                                                          // Calls the IDLE state.


        timeStunned = timeStunnedIni;


        if (slashFX.isPlaying)
        {
            slashFX.Stop(true);
        }
        
    }

    public void setAttack10()
    {
        Debug.Log("Attack10");

        crackGround.SetActive(true);

        ForcesDeactivation();

        rigidBody.AddForce(transform.forward * speedAttack10);      // Moves the player forward using physics.

        Attack10Action(attack10, tempAttack10);                     // Calls the AttackAction function, and give it the attack10 variable, and the tempAttack10 variable.

        anim.SetTrigger("Attack10");                                // Plays the attack10 animation.

		playerAudio.clip = swordSwipeClip;
		playerAudio.Play();

        state = PlayerStates.ATTACK_10;                             // Goes to the attack10 state.


        if (!slashFX.isPlaying)
        {
            slashFX.Play(true);
        }
    }
    
    public void setAttack01()
    {
        Debug.Log("Attack01");

        ForcesDeactivation();

        Attack01Action(attack01, tempAttack01);

        rigidBody.AddForce(transform.forward * speedAttack01);

        anim.SetTrigger("Attack01");

		playerAudio.clip = swordSwipeClip;
		playerAudio.Play();

        if (!slashFX.isPlaying)
        {
            slashFX.Play(true);
        }

        state = PlayerStates.ATTACK_01;
    }
    
    public void setSword10()
    {
        Debug.Log("Sword10");

        tempSword10Stun = tempSword10StunAux;

        Sword10Action(sword10, tempSword10);

        sword10CD = sword10CDAux;

        sword10ColorRValue = sword10ColorRValueAux;
        sword10ColorGValue = sword10ColorGValueAux;
        sword10ColorBValue = sword10ColorBValueAux;

        rigidBody.AddForce(transform.forward * speedSword10);

        anim.SetTrigger("Sword10");

        state = PlayerStates.SWORD_10;
    }

    public void setSword20()
    {
        Debug.Log("Sword20");

        ForcesDeactivation();

        Sword20Action(sword20, tempSword20);

        rigidBody.AddForce(transform.forward * speedSword20);

        anim.SetTrigger("Attack01");

        state = PlayerStates.SWORD_20;
    }
    
    public void setChain01()
    {
        Debug.Log("Chain01");

        ForcesDeactivation();

        Attack01Action(chain01, tempChain01);

        rigidBody.AddForce(transform.forward * speedChain01);

        anim.SetTrigger("Chain01");

        state = PlayerStates.CHAIN_01;
    }

    public void setChain02()
    {

        Debug.Log("Chain02");

        ForcesDeactivation();

        Chain02Action(chain02, tempChain02);

        rigidBody.AddForce(transform.forward * speedChain02);

        anim.SetTrigger("Attack01");

        state = PlayerStates.CHAIN_02;
    }
    
    public void setDash()
    {
        //Debug.Log("Dash");

		DashResistanceSlider.value -= resistancePerDash;

        anim.SetTrigger("IsDashing");                           // Plays the dash animation.

        rigidBody.AddForce(transform.forward * speedDash);      // Adds a force to the rigidbody of the player, in order of doing a dash.

        state = PlayerStates.DASH;                              // Goes to the dash state.
    }

    public void setDamaged(int damage)
	{
        damageImage.enabled = true;                             // Activation of the damage image.
        damageImage.color = flashColor;                         // Sets the color for the damageImage.

		temp = tempDamage;

		currentHealth -= damage;                                // Applies the damage recieved.

		playerAudio.clip = hurtClip;
        playerAudio.Play();                                     // Plays the hurt sound when the player gets hit.

        healthSlider.value = currentHealth;                     // Sets the value of the slider from the currentHealth of the player.

		if (currentHealth <= 0) setDead ();                     // Calls the setDead function if the player has died.
		else state = PlayerStates.DAMAGED;                      // If the player is still alive, calls the DAMAGED state.
	}

    public void setStunned()
    {
        anim.SetTrigger("Stunned");

        state = PlayerStates.STUNNED;
    }


    public void setDead()
	{
		currentHealth = 0;                                      // Sets the health to 0.

        sphereCollider.enabled = false;

        anim.SetTrigger("Die");                                 // Plays the die animation.

        playerAudio.clip = deathClip;                           // Plays the hurt sound when you die.
        playerAudio.Play();

        counterMenu = Time.realtimeSinceStartup;
        Debug.Log("dead");

        state = PlayerStates.DEAD;                              // Calls the DEAD state.
	}

	public void setVictory()
	{
        sphereCollider.enabled = false;
        capsuleCollider.enabled = false;
        counterMenu = Time.realtimeSinceStartup;
        Debug.Log("winning");
        state = PlayerStates.VICTORY;                           // Calls the VICTORY state.
	}

    // Functions 
    
    private void moveForward(float speed)
    {
        transform.localPosition += transform.forward * speed * Time.deltaTime;
    }

    private void moveBackward(float speed)
    {
        transform.localPosition -= transform.forward * speed * Time.deltaTime;
    }

    private void rotateRight()
    {
        transform.localRotation = Quaternion.Euler(transform.localRotation.x, playerRotation += 3, transform.localRotation.z);
    }

    private void rotateLeft()
    {
        transform.localRotation = Quaternion.Euler(transform.localRotation.x, playerRotation -= 3, transform.localRotation.z);
    }

    void Attack10Action(int damageDealt, float attackDuration)
    {
        attackDamage = damageDealt;                                 // Sets the amount of damage that the player does with this attack.

        attackStateCounter = attackDuration;                        // Sets the amount of time that the player has to be in the attackXX state.

		attackAction.enabled = true;                                // Activates the collider of the sword.
    }
    
    void Attack01Action(int damageDealt, float attackDuration)
    {
        attackDamage = damageDealt;                                 // Sets the amount of damage that the player does with this attack.

        attackStateCounter = attackDuration;                        // Sets the amount of time that the player has to be in the attackXX state.

        attack01Collider.enabled = true;          // Activates the collider of the sword.
    }

    void Sword10Action(int damageDealt, float attackDuration)
    {
        attackDamage = damageDealt;                                 // Sets the amount of damage that the player does with this attack.

        attackStateCounter = attackDuration;                        // Sets the amount of time that the player has to be in the attackXX state.

        attackAction.enabled = true;                                // Activates the collider of the sword.
    }

    void Sword20Action(int damageDealt, float attackDuration)
    {
        attackDamage = damageDealt;                                 // Sets the amount of damage that the player does with this attack.

        attackStateCounter = attackDuration;                        // Sets the amount of time that the player has to be in the attackXX state.

        attackAction.enabled = true;                                // Activates the collider of the sword.
    }

    void Chain01Action(int damageDealt, float attackDuration)
    {
        attackDamage = damageDealt;                                 // Sets the amount of damage that the player does with this attack.

        attackStateCounter = attackDuration;                        // Sets the amount of time that the player has to be in the attackXX state.
    }

    void Chain02Action(int damageDealt, float attackDuration)
    {
        attackDamage = damageDealt;                                 // Sets the amount of damage that the player does with this attack.

        attackStateCounter = attackDuration;                        // Sets the amount of time that the player has to be in the attackXX state.
    }
    
    public void ForcesDeactivation()
    {
        rigidBody.isKinematic = true;                               // Sets the isKinematic option to true.  
    }
}