using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    [SerializeField] Transform cam;
    [SerializeField] Transform orientation;

    float mouseX;
    float mouseY;

    float multiplier = 0.1f;

    float xRotation;
    float yRotation;

    private void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        MyInput();

       
    }

    void MyInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensX * multiplier;
        xRotation -= mouseY * sensY * multiplier;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
        transform.rotation = orientation.rotation;
    }
}
