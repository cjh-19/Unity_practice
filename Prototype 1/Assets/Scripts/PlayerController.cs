using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // public 접근제어자를 씀으로써 외부뿐만 아니라
    // 유니티 에디터에서 이 변수에 직접 접근하는 것이 가능해짐
    float speed = 15;
    float turnSpeed = 45.0f;

    float forwardInput;
    float horizontalInput;

    public Camera mainCamera;
    public Camera hoodCamera;
    public KeyCode switchKey;

    public string inputID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        forwardInput = Input.GetAxis("Vertical" + inputID);
        horizontalInput = Input.GetAxis("Horizontal" + inputID);

        // Move the vehicle forward
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput); // deltaTime : 현실 세계에서 흐른 시간을 반환해주는 함수
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
        // Rotate함수의 인자는 회전의 축, 회전의 각도

        if(Input.GetKeyDown(switchKey))
        {
            mainCamera.enabled = !mainCamera.enabled;
            hoodCamera.enabled = !hoodCamera.enabled;
        }
    }
}