using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isBoss = false;

    public float spawnInterval;
    private float nextSpawn;

    public int miniEnemySpawnCount;

    private SpawnManager spawnManager;

    public float speed = 3.0f; // �󸶸�ŭ�� �ӵ��� ������ ���ΰ�
    private Rigidbody enemyRb;
    private GameObject player;
    // ���� �÷��̾� ĳ���͸� �ν��ϰ� �װͿ� �����ϴ�
    // ������ �ؾ��ϹǷ� �÷��̾� ��ü�� ������

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

        if (isBoss)
        {
            spawnManager = FindObjectOfType<SpawnManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);
        // enemy�� �÷��̾ ���ؼ� �����ϵ��� �ؾ��Ѵ�.
        // ���� ���ؾ� ��
        // player.transfort.position : '-'������ ������ �÷��̾��� ��ġ, �������� enemy ��ġ
        // �÷��̾� ��ġ���� enemy ��ġ�� ���� ���Ͱ� ����
        // �̰��� nomalized : �Ÿ��� �������� �ʰ� ���⸸�� ��� ���� �������ͷ� �����
        // �� �������� ������ ũ�� : speed ��ŭ ���� �����Ѵ�.

        if(isBoss)
        {
            if(Time.time > nextSpawn)
            {
                nextSpawn = Time.time + spawnInterval;
                spawnManager.SpawnMiniEnemy(miniEnemySpawnCount);
            }
        }

        // ���� �������� �� ��ü ����
        if(transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
