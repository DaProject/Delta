using UnityEngine;
using System.Collections;

public class PSEnergy : MonoBehaviour {

    public ParticleSystem[] particles;

    public bool IsRender;
    public bool antRender;
    public ParticleSystem.EmissionModule em;

    // Use this for initialization
    void Start () {

        IsRender = false;
        antRender = false;

    }

    // Update is called once per frame
    void Update() {

        if (IsRender && !antRender) {

            for (int i = 0; i < particles.Length; i++)
            { 
                em = particles[i].emission;
                em.enabled = true;
            }

            antRender = true;
        }

        else if (!IsRender && antRender)
        {
            
            for (int i = 0; i < particles.Length; i++)
            {
                em = particles[i].emission;
                em.enabled = false;
            }

            antRender = false;
        }

	}
}
