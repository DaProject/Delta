using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PointCounter : MonoBehaviour
{
    public Text score;
    public int pointsCounter;

	// Use this for initialization
	void Start ()
    {
        score = GetComponent<Text>();
        pointsCounter = 0;
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        score.text = "" + pointsCounter;
    }
}
