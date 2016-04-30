using UnityEngine;
using System.Collections;

public class ExitScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("Exit");
	}
	
	// Update is called once per frame
	void Update ()
    {
        Exit();
	}

    public void Exit()
    {
        Application.Quit();
    }
}
