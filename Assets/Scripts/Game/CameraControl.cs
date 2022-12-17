using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float sensitivityMouse = 200f;
    [SerializeField]
    [Range(-90, 90)]
    private float minRotationX;
    [SerializeField]
    [Range(-90, 90)]
    private float maxRotationX;
    [ExecuteInEditMode]
    void OnValidate()
    {
        maxRotationX = Mathf.Clamp(maxRotationX, minRotationX, 90);
    }
    private float mouseX;
    private float mouseY;
    private float rotationAroundX = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * sensitivityMouse * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * sensitivityMouse * Time.deltaTime;

        rotationAroundX -= mouseY;
        rotationAroundX = Mathf.Clamp(rotationAroundX, minRotationX, maxRotationX);

        transform.localRotation = Quaternion.Euler(rotationAroundX, 0f, 0f);
        player.Rotate(mouseX * Vector3.up);
    }
}
