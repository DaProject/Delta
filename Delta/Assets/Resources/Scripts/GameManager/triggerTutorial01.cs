using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class triggerTutorial01 : MonoBehaviour
{
    public Transform playerTrans;

    public GameObject generator01;

    public GameObject howToMove;

    void Start()
    {
        transform.position = playerTrans.position;
        howToMove.SetActive(true);
    }

    void OnTriggerExit (Collider other)
    {
        if (other.tag == "Player")
        {
            if (!generator01.activeSelf) generator01.SetActive(true);
            howToMove.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}
