using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Change : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject backgroundObject; // 배경 이미지를 가진 GameObject를 가리키는 변수
    public Sprite newBackgroundSprite; // 변경할 새로운 배경 이미지

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // 점수가 3000 이상인 경우
        if (gameManager.score > 3000)
        {
            // 배경 이미지를 변경
            ChangeStage2Img();
        }
    }

    void ChangeStage2Img()
    {
        // Background GameObject의 SpriteRenderer를 찾아냄
        SpriteRenderer backgroundSpriteRenderer = backgroundObject.GetComponent<SpriteRenderer>();

        // 새로운 이미지를 할당
        backgroundSpriteRenderer.sprite = newBackgroundSprite;
    }
}
