using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float sensitivity;
    // Start is called before the first frame update
    void Start()
    {
        sensitivity = 1000f;
    }

    // Update is called once per frame
    void Update()
    {

        float VelocityY = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;
        float VelocityX = -Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;

        transform.Rotate(VelocityX, VelocityY, 0);
    }
}
