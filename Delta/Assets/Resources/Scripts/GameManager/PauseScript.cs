using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour
{

    public GameObject pauseMenu;
    private bool isEnabled = false;


    void Start()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
        isEnabled = false;

    }
    void Update()
    {
        // Enable pause menu
        if (Input.GetKeyDown(KeyCode.Escape) && !isEnabled)
        {
            pauseMenu.SetActive(true);
            isEnabled = true;
            Time.timeScale = 0.0f;
            Debug.Log("Paused");
        }

        // Disable pause menu
        else if (Input.GetKeyDown(KeyCode.Escape) && isEnabled)
        {
            Unpause();
        }
    }

    public void Unpause()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
        isEnabled = false;
        Debug.Log("Unpaused");
    }

}