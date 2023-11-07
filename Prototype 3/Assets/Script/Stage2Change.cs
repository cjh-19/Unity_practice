using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Change : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject backgroundObject; // ��� �̹����� ���� GameObject�� ����Ű�� ����
    public Sprite newBackgroundSprite; // ������ ���ο� ��� �̹���

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // ������ 3000 �̻��� ���
        if (gameManager.score > 3000)
        {
            // ��� �̹����� ����
            ChangeStage2Img();
        }
    }

    void ChangeStage2Img()
    {
        // Background GameObject�� SpriteRenderer�� ã�Ƴ�
        SpriteRenderer backgroundSpriteRenderer = backgroundObject.GetComponent<SpriteRenderer>();

        // ���ο� �̹����� �Ҵ�
        backgroundSpriteRenderer.sprite = newBackgroundSprite;
    }
}
