using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 배경이 무한히 반복하게 하는 스크립트
public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos; // 시작위치 고정
    private float repeatWidth; // 반복해야하는 너비의 간격을 미리 계산해서 저장하는 변수

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        // 박스컬라이더의 너비 절반크기만큼 가져옴.
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        // 원래 위치보다 왼쪽으로 이동(전체 배경 너비의 절반만큼 감소했다면) 재위치
        // 크기를 알기위해서 반드시 box collider를 추가해서 알아야한다
        if(transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}
