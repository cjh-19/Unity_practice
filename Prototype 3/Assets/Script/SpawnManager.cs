using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab; // obstacle�� �ش��ϴ� ������ �ϳ��� �����ϸ� �ǹǷ� ���� �迭 ���ص� ��
    private Vector3 spawnPos = new Vector3 (25, 0, 0); // �ֱ������� �����ϹǷ� �������ѵ�.

    private float startDelay = 2; // �ʱ� ������
    private float repeatRate = 2; // �ݺ� �ӵ�

    private PlayerController playerControllerScript; // �÷��̾� ��Ʈ�ѷ� ������Ʈ�� ����
    private int randomObstacle;

    // Start is called before the first frame update
    void Start()
    {
        // ��ֹ��� ��Ÿ���ٰ� ������� �ʰ� ���� �������� ��� ��Ÿ���� ��.
        // ���ڴ� ���� �Լ�, �ʱ� ������, �ݺ� �ӵ�
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);

        // �� ������ �ʱ�ȭ�� ���� ���� ����ȭ�� �ȿ� �ִ� Ư�� �̸��� �ִ� ������Ʈ�� �������� �Լ�
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
