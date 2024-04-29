using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraControll : MonoBehaviour
{
    // 회전할 각도
    public float rotationAngle = 90f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.O))
        {
            RotateCamera(-rotationAngle);
        }
        if (Input.GetKey(KeyCode.P))
        {
            RotateCamera(rotationAngle);
        }
    }
    void RotateCamera(float angle)
    {
        transform.Rotate(Vector3.up, angle);
    }
}
