using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab; // obstacle에 해당하는 프리팹 하나만 선언하면 되므로 굳이 배열 안해도 됨
    private Vector3 spawnPos = new Vector3 (25, 0, 0); // 주기적으로 등장하므로 고정시켜둠.

    private float startDelay = 2; // 초기 딜레이
    private float repeatRate = 2; // 반복 속도

    private PlayerController playerControllerScript; // 플레이어 컨트롤러 컴포넌트를 얻어옴
    private int randomObstacle;

    // Start is called before the first frame update
    void Start()
    {
        // 장애물이 나타났다가 사라지지 않고 일정 간격으로 계속 나타나게 함.
        // 인자는 생성 함수, 초기 딜레이, 반복 속도
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);

        // 이 변수를 초기화할 때는 현재 게임화면 안에 있는 특정 이름이 있는 오브젝트를 가져오는 함수
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle()
    {
        if(playerControllerScript.gameOver == false)
        {
            randomObstacle = Random.Range(0, obstaclePrefab.Length);
            Instantiate(obstaclePrefab[randomObstacle], spawnPos, obstaclePrefab[randomObstacle].transform.rotation);
        }
    }
}
