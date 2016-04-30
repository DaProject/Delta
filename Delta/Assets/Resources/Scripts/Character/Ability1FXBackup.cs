using UnityEngine;
using System.Collections;

public class Ability1FXBackup : MonoBehaviour {

    public GameObject crackGround;
    public float abilityCounter;

    public ParticleSystem thunders;

	public float iniScale;
	public float duration;
	public float finalScale;
	public float currentTime;
    public float shockwaveAlpha;

	public int startFrames;

	public Transform transform;
    public Material shockwaveMat;

    public Transform playerPosition;

    bool abilityActivated;


    // Use this for initialization
    void Start () {

        crackGround.SetActive(false);

        abilityActivated = false;

        thunders.Stop(true);

		currentTime = 0;

        //shockwaveMat = GetComponent<Renderer> ().material;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.T))
        {
			if (startFrames <= 0)
			{
				abilityActivated = true;

				crackGround.SetActive(true);

				abilityCounter = 240;
			
				thunders.Play(true);
            }

            else startFrames--;

        }

        if (abilityActivated == true)
        {
            abilityCounter--;

            if (currentTime <= duration)
            {

                transform.localScale = new Vector3(Easing.ExpoEaseOut(currentTime, iniScale, (finalScale - iniScale), duration),
                    Easing.ExpoEaseOut(currentTime, iniScale, (finalScale - iniScale), duration),
                    Easing.ExpoEaseOut(currentTime, iniScale, (finalScale - iniScale), duration));

                currentTime++;
            }

        }

        else if (abilityActivated == false) shockwaveAlpha = 0;

        if (abilityCounter <= 0)
		{
			crackGround.SetActive(false);

			if (thunders.isPlaying)
			{
				thunders.Stop(true);
			}


            shockwaveAlpha --;

            if (shockwaveAlpha <= 0)
            {
                shockwaveAlpha = 0;
            }

            abilityActivated = false;

            abilityCounter = 240;
        }
    }

    public void StartParticles()
    {

    }
}
