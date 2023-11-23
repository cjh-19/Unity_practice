using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f; // 얼마만큼의 속도로 움직일 것인가
    private Rigidbody enemyRb;
    private GameObject player;
    // 적이 플레이어 캐릭터를 인식하고 그것에 돌진하는
    // 동작을 해야하므로 플레이어 객체를 가져옴

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);
        // enemy는 플레이어를 향해서 돌진하도록 해야한다.
        // 힘을 가해야 함
        // player.transfort.position : '-'연산자 왼쪽은 플레이어의 위치, 오른쪽은 enemy 위치
        // 플레이어 위치에서 enemy 위치를 빼면 벡터가 나옴
        // 이것을 nomalized : 거리는 개입하지 않고 방향만을 얻기 위해 단위벡터로 만든다
        // 그 방향으로 일정한 크기 : speed 만큼 힘을 적용한다.

        // 적이 떨어지면 적 객체 제거
        if(transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
