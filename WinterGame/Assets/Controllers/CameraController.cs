using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 5f;
    public Transform Head;
    public Transform Player;

    private float xRotation = 0f;
    private float yRotation = 0f;


    private void Start()
    {
        Head.position = Player.transform.position;
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Head.position = Player.transform.position;

        float mouseY = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseX = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseX * mouseSensitivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += mouseY * mouseSensitivity;
        //yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        Head.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        //Head.Rotate(Vector3.up * mouseX);
        //Head.Rotate(Vector3.right * mouseY);

    }
}
