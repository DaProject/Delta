using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Generator : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public float timeBeforeExposed;
    public float emission;

    public Color baseColor;
    public Color finalColor;

    public GameObject generator;
    public DropGameObject dropGameObject;

    private BoxCollider boxCollider;
    private SphereCollider sphereCollider;
    private CapsuleCollider capsuleCollider;

    public GameObject howTo;

    public Material[] mat;
    public Renderer rend;
    
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        currentHealth = maxHealth;
        if (howTo != null) howTo.SetActive(true);
        rend.material.EnableKeyword("_EMISSION");
        baseColor = Color.green;
    }

    void Update()
    {
        if (timeBeforeExposed <= 0)
        {
            EmissionDamaged();
            ColliderActivation();
        }
        else
        {
            Emission();
            timeBeforeExposed -= Time.deltaTime;
            ColliderDeactivation();
        }

        if (currentHealth <= 0)
        {
            if (!generator.activeSelf && generator != null) generator.SetActive(true);  
            if (howTo != null) howTo.SetActive(false);
            if (dropGameObject != null) dropGameObject.DropDash();
            Destroy(this.gameObject);
        }
    }

    void ColliderActivation()
    {
        if (boxCollider != null) boxCollider.enabled = true;
        if (sphereCollider != null) sphereCollider.enabled = true;
        if (capsuleCollider != null) capsuleCollider.enabled = true;
    }

    void ColliderDeactivation()
    {
        if (boxCollider != null) boxCollider.enabled = false;
        if (sphereCollider != null) sphereCollider.enabled = false;
        if (capsuleCollider != null) capsuleCollider.enabled = false;
    }

    public void Emission()
    {
        //Debug.Log("Changing color");
        rend.material = mat[0];
        mat[0] = rend.material;
        EmissionChange();
    }

    public void EmissionDamaged()
    {
        rend.material = mat[1];
        mat[1] = rend.material;
        baseColor = Color.magenta;
        EmissionChange();
    }

    public void EmissionChange()
    {
        emission = Mathf.PingPong(Time.time, 1.0f);
        finalColor = baseColor * Mathf.LinearToGammaSpace(emission);
        rend.material.SetColor("_EmissionColor", finalColor);
    }
}
