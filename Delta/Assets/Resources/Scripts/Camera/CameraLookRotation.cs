using UnityEngine;
using System.Collections;

public class CameraLookRotation : MonoBehaviour
{
    public Transform characterTransform;
    public Transform cameraTransform;
    //public Transform headTransform;

    private Quaternion characterRotation;
    private Quaternion cameraRotation;
    //private Quaternion headRotation;

    private float xRotation, yRotation;

    public float XSensitivity;
    public float YSensitivity;

    public float smoothTime;

	// Use this for initialization
	void Start ()
    {
        characterRotation = characterTransform.localRotation;
        cameraRotation = cameraTransform.localRotation;
        //headRotation = headTransform.localRotation;
	}
	
	// Update is called once per frame
	void Update ()
    {
        yRotation = Input.GetAxis("Mouse X") * XSensitivity;
        xRotation = Input.GetAxis("Mouse Y") * YSensitivity;

        characterRotation *= Quaternion.Euler(0f, yRotation, 0f);
        cameraRotation *= Quaternion.Euler(-xRotation, 0f, 0f);
        //headRotation *= Quaternion.Euler(-xRotation, 0f, 0f);

        characterTransform.localRotation = Quaternion.Slerp(characterTransform.localRotation, characterRotation,
                    smoothTime * Time.deltaTime);
        cameraTransform.localRotation = Quaternion.Slerp(cameraTransform.localRotation, cameraRotation,
            smoothTime * Time.deltaTime);
        //headTransform.localRotation = Quaternion.Slerp(headTransform.localRotation, headRotation,
            //smoothTime * Time.deltaTime);

        UpdateCursor();
    }

    void UpdateCursor()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
