using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VictoryConditionEnergy : MonoBehaviour
{

    public PlayerManager playerManager;
    public GameObject player;
    public VictoryConditionEnergy victoryCondition;


    public int currentHealth;
    public Slider healthSlider;


    // Use this for initialization
    void Start ()
    {


        victoryCondition = GetComponent<VictoryConditionEnergy>();

        player = GameObject.FindGameObjectWithTag("Player");

        playerManager = player.GetComponent<PlayerManager>();

        currentHealth = 100;
    }
	
	// Update is called once per frame
	void Update ()
    {
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            playerManager.setVictory();

            victoryCondition.enabled = false;
        }
	}
}
