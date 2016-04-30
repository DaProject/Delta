using UnityEngine;
using System.Collections;

public class ResetScene : MonoBehaviour
{
    public PauseScript pauseScript;
    public int currentLevel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset(currentLevel);
        }
	
	}

    public void Reset(int level)
    {
        pauseScript.Unpause();
        Application.LoadLevel(level);
    }
}
