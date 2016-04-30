using UnityEngine;
using System.Collections;

public class Stunning : MonoBehaviour {

    EnemyPumpkinManager enemyManager;
    //public float stunOffset;
    //public float stunOffsetIni;

    void  Start ()
    {
        enemyManager = GetComponentInParent<EnemyPumpkinManager>();

        //stunOffset = stunOffsetIni;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //stunOffset -= Time.deltaTime;

            /*if (stunOffset <= 0)
            {
                other.GetComponent<PlayerManager>().setStunned();
            }
            */
            enemyManager.playerInRangeForStun = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            enemyManager.playerInRangeForStun = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && enemyManager.playerStunned)
        {
            other.GetComponent<PlayerManagerBackup>().setStunned();
        }
    }
}
