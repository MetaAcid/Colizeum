using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    
    private float mouseX;
    private float mouseY;

    public float sensitivityMouse = 200f;

    public Transform Player;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * sensitivityMouse * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * sensitivityMouse * Time.deltaTime;

        Player.Rotate(mouseX * new Vector3(0, 1, 0));
        transform.Rotate(-mouseY * new Vector3(1, 0, 0));

        //Vector3 rotation = transform.eulerAngles; 
        //rotation.x = Mathf.Clamp(transform.eulerAngles.x,0, 270);
        
        //transform.rotation = Quaternion.Euler(rotation);
    }
}
