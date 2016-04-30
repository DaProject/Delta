using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour
{
    public PauseScript pauseScript;

    public void LoadScene(int level)
	{
        //pauseScript.Unpause();

		Application.LoadLevel(level);
	}
}
