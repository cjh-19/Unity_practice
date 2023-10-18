using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce; // 점프 수치 조절
    public float gravityModifier; // 중력에 대한 영향을 수정할 수 있는 변수
    public bool isOnGround = true; // 캐릭터가 땅에 붙어있는지 점프한 상태인지 파악
    // true는 땅에 있는 상태

    // Start is called before the first frame update
    void Start()
    {
        // GetComponent 리지드 바디를 가져온는데 일종의 템플릿 함수로
        // 타입을 써주면 해당 타입의 컴포넌트를 반환해준다
        // 만약 추가되어있지 않은 컴포넌트를 넣는다면 none을 반환할 것이다.
        playerRb = GetComponent<Rigidbody>();

        // physics 모듈에서 중력값을 바꿀 때 사용
        // gracityModifier을 0으로 설정하면 무중력이 되므로 1이라는 수치로 설정
        Physics.gravity *= gravityModifier;

    }

    // Update is called once per frame
    void Update()
    {
        // 스페이스바를 눌렀을 때 if문 실행
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround) // 땅에 있으며 스페이스바를 눌렀을 때 점프
        {
            // AddForce함수를 적용하여 이 캐릭터를 특정방향으로 힘을 가해서 움직이게 한다.
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    // 이 함수는 객체가 다른 물체와 부딪혔을 때를 감지함
    {
        isOnGround = true;
    }
}
