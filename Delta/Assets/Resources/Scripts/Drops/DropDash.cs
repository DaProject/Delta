using UnityEngine;
using System.Collections;

public class DropDash : MonoBehaviour
{
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player")
        {
            Debug.Log("Got the Ability Slash!!");

            other.GetComponent<PlayerManager>().dashActive = true;
            //other.GetComponent<PlayerManager>().sword20Sprite.enabled = true;

            Destroy(this.gameObject);
		}
		
	}
}
