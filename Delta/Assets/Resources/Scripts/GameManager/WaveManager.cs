using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaveManager : MonoBehaviour {

    public Text actualWave;
    public PlayerManager playerManager;
    public OpenDoor openDoor;
    //public Text waveCommentary;
    //public int waveCounter;



    public void WaveOne()
    {
        actualWave.text = "Wave 1/3";
    }

    public void WaveTwo()
    {
        actualWave.text = "Wave 2/3";
        playerManager.newAbility.enabled = true;
        playerManager.sword10Sprite.SetActive (true);
        if (playerManager.alphaValue >= 0) playerManager.newAbility.color = new Color(1.0f, 1.0f, 1.0f, playerManager.alphaValue -= 0.2f * Time.deltaTime);
        playerManager.sword10Active = true;
    }

    public void WaveThree()
    {
        actualWave.text = "Wave 3/3";

        openDoor.waveCleared = true;
    }
}
