using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float rotationSpeed; // 회전속도

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // 좌우 입력값
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime); // 좌우로 해당 속도만큼 회전
    }
}
