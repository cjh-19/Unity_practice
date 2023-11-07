using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float score;
    private PlayerController playerControllerScript;
    public Transform startingPoint;
    public float lerpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        score = 0;

        playerControllerScript.gameOver = true;
        StartCoroutine(PlayIntro());
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerControllerScript.gameOver)
        {
            if (playerControllerScript.doubleSpeed)
            {
                score += 2;
            }
            else
            {
                score++;
            }
            Debug.Log("Score: " + score);
        }
    }

    IEnumerator PlayIntro()
    {
        // 시작 지점과 플레이어의 현재 위치를 가져온다
        Vector3 startPos = playerControllerScript.transform.position;
        Vector3 endPos = startingPoint.position;
        // 두 위치 사이 거리
        float journeyLength = Vector3.Distance(startPos, endPos);
        // 시작 시간
        float startTime = Time.time;

        // 이동 속도를 계산한다
        float distanceCovered = (Time.time - startTime) * lerpSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;

        // 빠르게 달리는 애니메이션 효과를 쓰되 속도를 줄인다
        playerControllerScript.GetComponent<Animator>().SetFloat("Speed_Multiplier", 0.5f);

        // 목적지에 도달할 때까지 이동한다.
        while(fractionOfJourney < 1)
        {
            /// 현재까지 이동한 거리 업데이트
            distanceCovered = (Time.time - startTime) * lerpSpeed;
            // 현재 이동한 거리의 전체 이동 거리에 대한 비율 계산
            fractionOfJourney = distanceCovered / journeyLength;
            // 플레이어를 시작 지점에서 목적지까지 부드럽게 이동시킨다
            playerControllerScript.transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
            // 다음 프레임까지 기다린다
            yield return null;
        }

        // 애니메이션 속도를 되돌리고, 게임 오버 상태를 false로 설정한다
        playerControllerScript.GetComponent<Animator>().SetFloat("Speed_Multiplier", 1.0f);
        playerControllerScript.gameOver = false;
    }
}
