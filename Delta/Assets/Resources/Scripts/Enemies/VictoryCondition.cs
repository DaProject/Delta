using UnityEngine;
using System.Collections;

public class VictoryCondition : MonoBehaviour
{
    public EnemyPumpkinManager enemyPumpkinManager;
    public PlayerManagerBackup playerManagerBackup;
    public GameObject player;
    public VictoryCondition victoryCondition;

	// Use this for initialization
	void Start ()
    {
        enemyPumpkinManager = GetComponent<EnemyPumpkinManager>();
        victoryCondition = GetComponent<VictoryCondition>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerManagerBackup = player.GetComponent<PlayerManagerBackup>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (enemyPumpkinManager.currentHealth <= 0)
        {
            playerManagerBackup.setVictory();
            victoryCondition.enabled = false;
        }
	}
}
