using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 30; // 미리 정의된 속도로 왼쪽으로 계속 이동하게 함
    private PlayerController playerControllerScript; // 플레이어 컨트롤러 컴포넌트를 얻어옴

    private float leftBound = -15;

    // Start is called before the first frame update
    void Start()
    {
        // 이 변수를 초기화할 때는 현재 게임화면 안에 있는 특정 이름이 있는 오브젝트를 가져오는 함수
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // playercontrolller의 gameOver변수는 public이므로 접근 가능
        // gameOver가 false일 때만 움직이게 한다.
        if(playerControllerScript.gameOver == false) 
        {
            if (playerControllerScript.doubleSpeed)
            {
                transform.Translate(Vector3.left * Time.deltaTime * (speed * 2));
            }
            else {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
        }

        // 상자가 바닥으로 떨어지면 제거한다
        if(transform.position.x < leftBound && gameObject.CompareTag("Obstacle")) 
        {
            Destroy(gameObject);
        }
    }
}
