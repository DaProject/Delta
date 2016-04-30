using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    [Header("Controller")]
    private PlayerManager playerManager;
    private PlayerAnimation playerAnimation;
    private PlayerController playerController;
    private CharacterBehaviour characterBehaviour;
    public Ability1FX stunAbility;

    public int attackDamage;

    public int damageAttack10;                      // Variable with the damage of the attack10 (sword).
    public int damageSword10;                       // Variable with the damage of the sword10 (sword hability).
    public int damageAttack01;                      // Variable with the damage of the attack01 (chain).
    public int damageChain01;                       // Variable with the damage of the chain01 (chain hability).
    public float attack10ColliderRadius;
    public float sword10CD;
    public float sword10CDAux;
    public float tempSword10Stun;                   // Counter that determinates when the sword10 attack is going to stun the enemies.

    public bool trailActivation;

	// Use this for initialization
	void Start ()
    {
        playerManager = GetComponent<PlayerManager>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerController = GetComponent<PlayerController>();
        characterBehaviour = GetComponent<CharacterBehaviour>();
    }

    public void Attack10(int damageDealt)
    {
        Debug.Log("Attack10");

        trailActivation = true;

        playerManager.attack10Collider.enabled = true;          // Activates the collider of the Attack10 attack.

        playerAnimation.Attack10Animation();                    // Calls the Attack10Animation function from the PlayerAnimation script.

        attackDamage = damageDealt;
    }

    public void Attack01(int damageDealt)
    {
        Debug.Log("Attack01");

        playerManager.attack01Collider.enabled = true;          // Activates the collider of the Attack10 attack.

        playerManager.attack01Collider.radius += attack10ColliderRadius * Time.deltaTime;        // Makes the attack01Collider get bigger during the attack01Behaviour.

        playerAnimation.Attack01Animation();                    // Calls the Attack01Animation function from the PlayerAnimation script.

        attackDamage = damageDealt;
    }

    public void Sword10(int damageDealt)
    {
        Debug.Log("Sword10");

        playerManager.sword10Collider.enabled = true;       // Activates the collider of the Attack10 attack.

        playerAnimation.Sword10Animation();                 // Calls the Sword10Animation function from the PlayerAnimation script.

        stunAbility.stunActivated = true;

        attackDamage = damageDealt;
    }

    public void Chain01(int damageDealt)
    {
        Debug.Log("Chain01");

        playerAnimation.Chain01Animation();                 // Calls the Chain10Animation function from the PlayerAnimation script.

        attackDamage = damageDealt;
    }

    public void Dash()
    {
        Debug.Log("dashing");
        playerAnimation.DashAnimation();
        playerManager.playerSphereCollider.enabled = false;
        playerManager.playerCapsuleCollider.enabled = false;
        //playerController.rigidBody.useGravity = false;
    }
}
