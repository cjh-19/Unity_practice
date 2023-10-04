using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // public ���������ڸ� �����ν� �ܺλӸ� �ƴ϶�
    // ����Ƽ �����Ϳ��� �� ������ ���� �����ϴ� ���� ��������
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
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput); // deltaTime : ���� ���迡�� �帥 �ð��� ��ȯ���ִ� �Լ�
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
        // Rotate�Լ��� ���ڴ� ȸ���� ��, ȸ���� ����

        if(Input.GetKeyDown(switchKey))
        {
            mainCamera.enabled = !mainCamera.enabled;
            hoodCamera.enabled = !hoodCamera.enabled;
        }
    }
}