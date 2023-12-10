using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // �ؽ�Ʈ�� �ٷ�� ���� ��� �߰�

public class GameManager : MonoBehaviour
{
    // ���ӸŴ������� �������� ��ǥ���� �� ������Ʈ�� ����Ʈȭ �Ѵ�.
    public List<GameObject> targets;
    // �󸶳� ���� ������ �� ���ΰ�
    private float spawnRate = 1.0f;

    private int score; // ���� ���� ���ھ�
    // ���ھ� �ؽ�Ʈ ��ü ����
    public TextMeshProUGUI scoreText;


    // Start is called before the first frame update
    void Start()
    {
        // �ڷ�ƾ : ������ ���������� �������� ��ũ��Ʈ�� �����ϴ� ���� ȿ���� ��
        StartCoroutine(SpawnTarget());
        
        score = 0;
        scoreText.text = "Score: " + score;
    }

    IEnumerator SpawnTarget()
    {
        while(true)
        {
            // spawnRate ��ŭ �����ٰ� �ڵ带 ���� -> �̰��� ��� �ݺ��Ͽ� ���������� �����ϵ���  ��
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]); // index ��ȣ�� ��ü ����
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // target���� ������ �� �ֵ��� public���� ����
    public void UpdateScore(int scoreToAdd) // �Լ� ���ڴ� ��ŭ ������
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
}
