using UnityEngine;
using System.Collections;

public class ActivationGeneration : MonoBehaviour
{
    public GameObject generator_01;
    public GameObject generator_02;
    public GameObject generator_03;
    public GameObject generator_04;
    public GameObject generator_05;
    public GameObject generator_06;

    void Update()
    {
        if (Input.GetKeyDown("[1]")) generator_01.SetActive(true);
        else if (Input.GetKeyDown("[2]")) generator_02.SetActive(true);
        else if (Input.GetKeyDown("[3]")) generator_03.SetActive(true);
        else if (Input.GetKeyDown("[4]")) generator_04.SetActive(true);
        else if (Input.GetKeyDown("[5]")) generator_05.SetActive(true);
        else if (Input.GetKeyDown("[6]")) generator_06.SetActive(true);
    }
}
