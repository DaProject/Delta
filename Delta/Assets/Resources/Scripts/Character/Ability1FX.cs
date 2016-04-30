using UnityEngine;
using System.Collections;

public class Ability1FX : MonoBehaviour {

    public GameObject crackGround;
    public float abilityCounter;

    public ParticleSystem thunders;

	public float iniScale;
	public float duration;
	public float finalScale;
	public float currentTime;
    public float shockwaveAlpha;

    public bool stunActivated;

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

        stunActivated = false;

        abilityCounter = 2;

        //shockwaveMat = GetComponent<Renderer> ().material;
    }
	
	// Update is called once per frame
	void Update () {



        if (stunActivated )
        {
                abilityActivated = true;

                crackGround.SetActive(true);

                abilityCounter -= Time.deltaTime;

                thunders.Play(true);

                if (currentTime <= duration)
                {

                    transform.localScale = new Vector3(Easing.ExpoEaseOut(currentTime, iniScale, (finalScale - iniScale), duration),
                        Easing.ExpoEaseOut(currentTime, iniScale, (finalScale - iniScale), duration),
                        Easing.ExpoEaseOut(currentTime, iniScale, (finalScale - iniScale), duration));

                    currentTime++;
                }

            
        }

        if (abilityCounter <= 0) stunActivated = false;

        if (!stunActivated)
        {
           

            Debug.Log("STUN BOOL FALSE");
            crackGround.SetActive(false);

            if (thunders.isPlaying)
            {
                thunders.Stop(true);
            }

            transform.localScale = new Vector3(0, 0, 0);

            currentTime = 0;

            abilityActivated = false;

            abilityCounter = 2;
       
        }
    }
}
