using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour
{
    // �����ϰ��� �ϴ� ��ü�� ������ ����
    public GameObject enemyPrefab;
    // powerup ������ ����
    public GameObject powerupPrefab;
    // ������ ��ġ���� ����
    private float spawnRange = 9;

    // ���� ����� ī��Ʈ
    public int enemyCount;
    public int waveNumber = 1; // ���� �Ź� ����� ����

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);

        SpawnEnemyWave(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length; // object"s". s���� ����
        // ��� ���� �����ߴٸ�
        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);

            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }
    }
    
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        // �������� ���� ����
        for(int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }

    }

    // ������ ���� �������� �������ִ� �Լ�
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }
}