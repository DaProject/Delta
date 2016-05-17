using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyPumpkinManager : MonoBehaviour
{
    
    // States of the enemy
    public enum EnemyStates {AWAKE, IDLE, ACTIVE, ATTACK, STUNATTACK, DAMAGED, STUNNED, DEAD}
    [Header("States")]
    public EnemyStates state;

    // Health
    [Header("Health")]
    public int maxHealth;
    public int currentHealth;
    public int points;

    // NavMesh
    [Header("NavMesh")]
    NavMeshAgent nav;                           // NavMesComponent 
    GameObject player;                          // Player
    
    // Damage
    [Header("Attack")]
    public int attackDamage;                    // Auxiliar variable that gets the value of the differents attacks. After it is used to apply the damage to the player.
    public int attackMelee;                     // Variable with the damage of the enemy attack.
    public bool playerInRange;                  // Bool that is true when the player is in attack range.
    public bool playerAttacked;                 // Bool created in order to control how many times the enemy does damage with the same attack.
    
    // Sounds
    [Header("Sounds")]
    public AudioClip hurtClip;
    //AudioSource enemyAudio;

    // Timers
    [Header("Timers")]
    public float temp;
    public float tempDead;
    public float tempDamage;                    // Counter that determinates how much time the enemy has to be in the DAMAGED state.               
    public float tempAttackMelee;               // Counter that reflects how much the enemy attack longs.
    public float tempStunned;
    public float attackStateCounter;            // Auxiliar variable that says how much time the enemy has to be in the ATTACK state. It gets the value from the counter of the attack.

    [Header("Stun")]
    public float onStunTimerIni;
    public float stunCooldown;                  // Counter that says what is que cooldown for the stun.
    public float stunCooldownIni;               // Auxiliar variable that stores the stunCooldown. Used for reseting.
    public float stunOffset;                    // Counter that control when the enemy actually stuns the player. Used to avoid stunning the player for the very beggining of the stun.
    public float stunOffsetIni;                 // Auxiliar variable that stores the stunOffset. Used for reseting.
    public bool playerInRangeForStun;           // Bool that says if the player is in range for stun or not.
    public bool playerStunned;                  // Boll that says if the enemy has finished his stun.
    private float onStunTimer;


    // Control enemy
    [Header("Control")]
    public GameObject enemy;
    public CapsuleCollider capsuleCollider;     // Capsule collider of the enemy.    
    public SphereCollider sphereCollider;       // Sphere collider of the enemy.
    public SphereCollider leftHandAttack1;      // Sphere collider of the left hand of the enemy. Used to detect if the attack has connected with the player.
    public GameObject pointsText;
    public PointCounter score;
    public Material[] material;
    public Renderer mat;

    // Animations
    Animator anim;                              // Animator from the enemy.

    // Particles
    public ParticleSystem hitSparkParticle;
    public ParticleSystem stunThunderParticle;
	public ParticleSystem blood;

    // Scripts calls
    PlayerManager playerManager;                // PlayerManager script

	// Use this for initialization
	void Start ()
    {
        setAwake();
	}
	
	// Update is called once per frame
	void Update ()
    {
        switch (state)
        {
            case EnemyStates.AWAKE:
                AwakeBehaviour();
                break;
            case EnemyStates.IDLE:
                IdleBehaviour();
                break;
            case EnemyStates.ACTIVE:
                ActiveBehaviour();
                break;
            case EnemyStates.ATTACK:
                AttackBehaviour();
                break;
            case EnemyStates.STUNATTACK:
                StunAttackBehaviour();
                break;
            case EnemyStates.DAMAGED:
                DamagedBehaviour();
                break;
            case EnemyStates.STUNNED:
                StunnedBehaviour();
                break;
            case EnemyStates.DEAD:
                DeadBehaviour();
                break;
        }

        stunCooldown -= Time.deltaTime;
        
    }

    // Behaviorus
    private void AwakeBehaviour()
    {
        setActive();
    }

    private void IdleBehaviour()
    {
        anim.SetTrigger("Idle");                                                // It triggers the enemy Idle animation.
    }

    private void ActiveBehaviour()
    {
        ControllerAction();                                                     // Calls the ControllerAction function when the enemy has to move.

        if (playerManager.currentHealth == 0)                                   // Checks when the player dies. Goes to setIdle and stops attacking and stunning him.
        {
            playerInRange = false;

            setIdle();
        }

        playerAttacked = false;

        playerStunned = false;

        if (playerInRange) setAttack();                                         // Calls the setAttack function if the player is in range to attack.

        if (playerInRangeForStun && stunCooldown <= 0) setStunAttack();         // Calls the setStunAttack when the player is in range to attack, and the cooldown has refreshed.
    }

    private void AttackBehaviour()
    {
        attackStateCounter -= Time.deltaTime;                                   // Starts the countdown after the attack has been done.

        if (attackStateCounter <= 0) setActive();                               // Goes back to setIdle if the enemy has not attack for a small amount of time.
    }

    void StunAttackBehaviour()
    {   
        onStunTimer -= Time.deltaTime;

        stunOffset -= Time.deltaTime;

        if (onStunTimer <= 0) setActive();                                      // Calls the setActive when the enemy has finished his stun.

        if (stunOffset <= 0) playerStunned = true;                              // Activates the playerStunned when the enemy has finished his stun.

    }

    private void DamagedBehaviour()
    {
        temp -= Time.deltaTime;

        if (temp <= 0) setActive();
    }

    private void StunnedBehaviour()
    {
        temp -= Time.deltaTime;

        if (temp <= 0) setActive();
    }

    private void DeadBehaviour()
    {
        tempDead -= Time.deltaTime;

        if (tempDead <= 0) enemy.SetActive(!enemy.activeSelf);                  // The enemy deactivates himself when he dies.
    }

    // Sets
    public void setAwake()
    {
        currentHealth = maxHealth;

        player = GameObject.FindGameObjectWithTag("Player");                    // Finds the gameobject with the tag "Player".

        playerManager = player.GetComponent<PlayerManager>();             // Gets the script PlayerManager of the player.
        pointsText = GameObject.FindGameObjectWithTag("Score");
        score = pointsText.GetComponent<PointCounter>();

        nav = GetComponent<NavMeshAgent>();                                     // Gets the NavMeshAgent component.

        playerInRange = false;                                                  // Initalize the playerInRange bool to false.

        playerAttacked = false;                                                 // Initialize the playerAttacked bool to false.

        onStunTimer = onStunTimerIni;                                           // Initialize the onStunTimer.

        stunCooldown = stunCooldownIni;                                         // Initialize the stunCooldown.

        stunOffset = stunOffsetIni;                                             // Initialize the stunOffset.

        stunThunderParticle.Stop();

        hitSparkParticle.Stop();

		blood.Stop(true);

        //enemyAudio = GetComponent<AudioSource>();                             // Gets the AudioSource component from the enemy.

        //rigidBody = GetComponent<Rigidbody>();                                // Gets the rigidbody component from the enemy.
        //capsuleCollider = GetComponent<CapsuleCollider>();
        //sphereCollider = GetComponent<SphereCollider>();
        //leftHandAttack1 = GetComponentInChildren<SphereCollider>();           // Gets the SphereCollider of the leftHandAttack1 children.

        anim = GetComponent<Animator>();                                        // Gets the animator component from the enemy.

        state = EnemyStates.AWAKE;                                              // Goes to the AWAKE state.
    }

    public void setIdle()
    {
        capsuleCollider.enabled = true;                         // Activates the capsuleCollider. It keeps activated by default.
        sphereCollider.enabled = true;                          // Activates the sphereCollider. It keeps activated by default.
        leftHandAttack1.enabled = false;                        // Deactivates the leftHandAttack coollider. It keeps deactivated by default.

        state = EnemyStates.IDLE;
    }

    public void setActive()
    {
        leftHandAttack1.enabled = false;

        anim.SetBool("Attack", false);                          // Sets the Attack bool for the attack animation to false.

        nav.enabled = true;

        onStunTimer = onStunTimerIni;

        stunOffset = stunOffsetIni;

        if (stunThunderParticle.isPlaying)
        {
            stunThunderParticle.Stop();
        }

        if (hitSparkParticle.isPlaying)
        {
            hitSparkParticle.Stop();
        }

		if (blood.isPlaying)
		{
			blood.Stop(true);
		}

        mat.material = material[0];

        state = EnemyStates.ACTIVE;                             // Goes to the ACTIVE state.
    }

    public void setAttack()
    {
        AttackAction(attackMelee, tempAttackMelee);             // Calls the AttackAction function, and give it the damage  fo the attack, and the time that longs.

        anim.SetBool("Attack", true);                           // Sets the Attack bool for the attack animation to true.

        anim.SetBool("Run", false);                             // Sets the Run bool for the run animation to true.

        state = EnemyStates.ATTACK;                             // Goes to the ATTACK state.
    }

    public void setStunAttack()
    {
        anim.SetTrigger("isStunning");

        leftHandAttack1.enabled = false;                        // Deactivates the leftHandAttack1 collider, since it keeps activated if the enemy doesn't go to the activeState. This can happen if the player is always in range to stun.

        stunCooldown = stunCooldownIni;

        state = EnemyStates.STUNATTACK;
    }

    public void setDamaged(int damage)
    {
        temp = tempDamage;

        if (!hitSparkParticle.isPlaying)
        {
            hitSparkParticle.Play();
        }

        mat.material = material[1];

        blood.Play(true);

        currentHealth -= damage;                                // Applies the damage recieved

        anim.SetTrigger("Damaged");                             // It triggers the enemy Damaged animation.

        if (currentHealth <= 0) setDead();                      // Calls the setDead function if the enemy has died

        else state = EnemyStates.DAMAGED;                       // If the enemy is still alive, calls the DAMAGED state
    }

    public void setStunned()
    {
        // TODO: implement the animation
        Debug.Log("Enemy stunned :(");

        temp = tempStunned;

        nav.enabled = false;

        state = EnemyStates.STUNNED;
    }

    public void setDead()
    {
        currentHealth = 0;                                      // Sets the health to 0.

        capsuleCollider.enabled = false;
        sphereCollider.enabled = false;
        leftHandAttack1.enabled = false;

        score.pointsCounter += points;

        anim.SetTrigger("Die");                                 // Plays the die animation.

        state = EnemyStates.DEAD;                               // Calls the DEAD state.
    }

    public void ControllerAction()
    {
        nav.SetDestination(player.transform.position);                      // Sets the destination of the navmesh to the actual player position. The enemy is goin to go there.

    
        anim.SetBool("Run", true);                                          // Sets the Run bool for the run animation to true.
    }

    private void AttackAction(int damageDealt, float attackDuration)
    {
        //Debug.Log("attacking player");

        leftHandAttack1.enabled = true;                                     //Sets to true the collider of the leftHandAttack1.

        attackDamage = damageDealt;                                         // Sets the amount of damage that the enemy does with this attack.

        attackStateCounter = attackDuration;                                // Sets the amount of time that the enemy has to be in the attack state.
    }

    void  OnTriggerStay(Collider other)
    {
        if (other.tag == "Player") playerInRange = true;                    // Sets the playerInRange to true if the player has been detected around the enemy.
    }

    void OnTriggerExit(Collider other)
    {
        //Debug.Log("player lost");
        if (other.tag == "Player") playerInRange = false;                   // Sets the playerInRange to true if the player has exit the area detection of the enemy.
    }
}
