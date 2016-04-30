using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public GameObject EnemyType01;
    public GameObject EnemyType02;
    public GameObject EnemyType03;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Instantiate(EnemyType01, transform.position, Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Instantiate(EnemyType02, transform.position, Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            Instantiate(EnemyType03, transform.position, Quaternion.identity);
        }

    }
}
