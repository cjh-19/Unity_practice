using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // 태그가 플레이어라면 life를 제거
        if (other.CompareTag("Player"))
        {
            gameManager.AddLive(-1);
            Destroy(gameObject);
        }
        // 태그가 동물인지 확인하고, 만약 그렇다면 점수를 추가
        else if (other.CompareTag("Animal") && gameObject.CompareTag("Food"))
        {
            other.GetComponent<AnimalHunger>().FeedAnimal(1); // AnimalHunger스크립트를 열어 음식이 동물을 칠 때의 충돌을 조정
            Destroy(gameObject); // 자신 제거 - 동물제거
        }
    }
}
