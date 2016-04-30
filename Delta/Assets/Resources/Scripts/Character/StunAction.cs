using UnityEngine;
using System.Collections;

public class StunAction : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("Enemy stunned :)");
            other.GetComponent<EnemyPumpkinManager>().setStunned();
        }
    }
}
