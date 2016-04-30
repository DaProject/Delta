using UnityEngine;
using System.Collections;

public class DropSword10Trigger : MonoBehaviour
{
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player")
        {
            Debug.Log("Got the Ability Slash!!");

            other.GetComponent<PlayerManagerBackup>().sword10Active = true;
            other.GetComponent<PlayerManagerBackup>().sword10Sprite.enabled = true;

			Destroy(this.gameObject);
		}
		
	}
}
