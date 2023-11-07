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
        // ���� ������ �÷��̾��� ���� ��ġ�� �����´�
        Vector3 startPos = playerControllerScript.transform.position;
        Vector3 endPos = startingPoint.position;
        // �� ��ġ ���� �Ÿ�
        float journeyLength = Vector3.Distance(startPos, endPos);
        // ���� �ð�
        float startTime = Time.time;

        // �̵� �ӵ��� ����Ѵ�
        float distanceCovered = (Time.time - startTime) * lerpSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;

        // ������ �޸��� �ִϸ��̼� ȿ���� ���� �ӵ��� ���δ�
        playerControllerScript.GetComponent<Animator>().SetFloat("Speed_Multiplier", 0.5f);

        // �������� ������ ������ �̵��Ѵ�.
        while(fractionOfJourney < 1)
        {
            /// ������� �̵��� �Ÿ� ������Ʈ
            distanceCovered = (Time.time - startTime) * lerpSpeed;
            // ���� �̵��� �Ÿ��� ��ü �̵� �Ÿ��� ���� ���� ���
            fractionOfJourney = distanceCovered / journeyLength;
            // �÷��̾ ���� �������� ���������� �ε巴�� �̵���Ų��
            playerControllerScript.transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
            // ���� �����ӱ��� ��ٸ���
            yield return null;
        }

        // �ִϸ��̼� �ӵ��� �ǵ�����, ���� ���� ���¸� false�� �����Ѵ�
        playerControllerScript.GetComponent<Animator>().SetFloat("Speed_Multiplier", 1.0f);
        playerControllerScript.gameOver = false;
    }
}
