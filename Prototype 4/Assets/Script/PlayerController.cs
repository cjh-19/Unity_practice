using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb; // ��ü�� ��ü�� ������ ��Ʈ��
    private GameObject focalPoint; // focalPoint ��ü�� ������
    public float speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>(); // ������Ʈ�� ������
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical"); // ���� �Է°�
        // focalPoint�� ������ ���Ͽ� �յڷ� �����̰� �Ѵ�.
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput); // ���� �յڷ� �̵��ϰ� ��
    }
}
