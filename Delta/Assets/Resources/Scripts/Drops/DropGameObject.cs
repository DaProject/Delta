using UnityEngine;
using System.Collections;

public class DropGameObject : MonoBehaviour {

	public GameObject drop;
	public Transform trans;
	private float oneTimeDrop;

	// Use this for initialization
	void Start () 
	{
		oneTimeDrop = 0;
	}

	// Update is called once per frame
	void Update () 
	{
		if (trans.GetComponent<EnemyPumpkinManager> ().currentHealth == 0)
		{
			oneTimeDrop ++;

			if (oneTimeDrop <= 1) Instantiate(drop, transform.position, Quaternion.identity);
		}
	}
}

