using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour
{
    // �����ϰ��� �ϴ� ��ü�� ������ �迭
    public GameObject[] enemyPrefab;
    // powerup ������ ����
    public GameObject[] powerupPrefab;
    // ������ ��ġ���� ����
    private float spawnRange = 9;

    // ���� ����� ī��Ʈ
    public int enemyCount;
    public int waveNumber = 1; // ���� �Ź� ����� ����

    public GameObject bossPrefab;
    public GameObject[] miniEnemyPrefabs;
    public int bossRound;

    // Start is called before the first frame update
    void Start()
    {
        int randomPowerup = Random.Range(0, powerupPrefab.Length);
        Instantiate(powerupPrefab[randomPowerup], GenerateSpawnPosition(), powerupPrefab[randomPowerup].transform.rotation);

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

            if(waveNumber % bossRound == 0)
            {
                SpawnBossWave(waveNumber);
            }
            else
            {
                SpawnEnemyWave(waveNumber);
            }

            int randomPowerup = Random.Range(0, powerupPrefab.Length);
            Instantiate(powerupPrefab[randomPowerup], GenerateSpawnPosition(), powerupPrefab[randomPowerup].transform.rotation);
        }
    }
    
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        // �������� ���� ����
        for(int i = 0; i < enemiesToSpawn; i++)
        {
            int randomEnemy = Random.Range(0, enemyPrefab.Length);
            Instantiate(enemyPrefab[randomEnemy], GenerateSpawnPosition(), enemyPrefab[randomEnemy].transform.rotation);
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

    void SpawnBossWave(int currentRound)
    {
        int miniEnemysToSpawn;

        if(bossRound != 0)
        {
            miniEnemysToSpawn = currentRound / bossRound;
        }
        else
        {
            miniEnemysToSpawn = 1;
        }

        var boss = Instantiate(bossPrefab, GenerateSpawnPosition(), bossPrefab.transform.rotation);
        boss.GetComponent<Enemy>().miniEnemySpawnCount = miniEnemysToSpawn;

    }

    public void SpawnMiniEnemy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int randomMini = Random.Range(0, miniEnemyPrefabs.Length);
            Instantiate(miniEnemyPrefabs[randomMini], GenerateSpawnPosition(), miniEnemyPrefabs[randomMini].transform.rotation);
        }
    }

}