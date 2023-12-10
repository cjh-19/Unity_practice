using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // �ؽ�Ʈ�� �ٷ�� ���� ��� �߰�
using UnityEngine.SceneManagement; // ���� ������� ���� ���
using UnityEngine.UI; // ��ư ��ü�� ���� ���

public class GameManager : MonoBehaviour
{
    // ���ӸŴ������� �������� ��ǥ���� �� ������Ʈ�� ����Ʈȭ �Ѵ�.
    public List<GameObject> targets;
    // �󸶳� ���� ������ �� ���ΰ�
    private float spawnRate = 1.0f;

    private int score; // ���� ���� ���ھ�
    public bool isGameActive; // ���� ���� ����

    // ���ھ� �ؽ�Ʈ ��ü ����
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartGame(int difficulty)
    {
        // ���̵� ���� ���ڸ� �޾� spawnRate ���� �����Ѵ�
        spawnRate /= difficulty;

        // StartCoroutine �ڿ� ������ �ȵ�.
        // �ڷ�ƾ�� ����� ���� false���̹Ƿ� ���� true�� �ϰ�
        // �ڷ�ƾ�� �����ؾ���.
        isGameActive = true;

        // �ڷ�ƾ : ������ ���������� �������� ��ũ��Ʈ�� �����ϴ� ���� ȿ���� ��
        StartCoroutine(SpawnTarget());

        score = 0;
        scoreText.text = "Score: " + score;

        titleScreen.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive= false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator SpawnTarget()
    {
        while(isGameActive)
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
