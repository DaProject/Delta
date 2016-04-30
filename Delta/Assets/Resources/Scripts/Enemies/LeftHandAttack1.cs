using UnityEngine;
using System.Collections;

public class LeftHandAttack1 : MonoBehaviour {

    public EnemyPumpkinManager enemyPumpkinManager;

    void Start()
    {
        enemyPumpkinManager = transform.root.GetComponent<EnemyPumpkinManager>();
    }
    
	void OnTriggerEnter (Collider other)
	{
        if (other.tag == "Player")
        {
            if (!enemyPumpkinManager.playerAttacked)
            {
                enemyPumpkinManager.playerAttacked = true;
                Debug.Log("Player Attacked");
                other.GetComponent<PlayerManager>().setDamaged(transform.root.GetComponent<EnemyPumpkinManager>().attackDamage);
            }
        }
	}
}
