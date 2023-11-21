using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb; // 물체의 강체를 가져와 컨트롤
    private GameObject focalPoint; // focalPoint 객체를 가져옴
    public float speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>(); // 컴포넌트를 가져옴
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical"); // 수직 입력값
        // focalPoint의 앞쪽을 향하여 앞뒤로 움직이게 한다.
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput); // 공을 앞뒤로 이동하게 함
    }
}
