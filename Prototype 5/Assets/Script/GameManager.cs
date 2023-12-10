using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // 텍스트를 다루기 위한 모듈 추가
using UnityEngine.SceneManagement; // 게임 재시작을 위한 모듈
using UnityEngine.UI; // 버튼 객체를 위한 모듈

public class GameManager : MonoBehaviour
{
    // 게임매니저에서 스포닝할 목표물을 할 오브젝트를 리스트화 한다.
    public List<GameObject> targets;
    // 얼마나 빨리 스포닝 할 것인가
    private float spawnRate = 1.0f;

    private int score; // 현재 실제 스코어
    public bool isGameActive; // 게임 진행 유무

    // 스코어 텍스트 객체 선언
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
        // 난이도 조절 인자를 받아 spawnRate 값을 변경한다
        spawnRate /= difficulty;

        // StartCoroutine 뒤에 있으면 안됨.
        // 코루틴이 진행될 때는 false값이므로 먼저 true로 하고
        // 코루틴을 실행해야함.
        isGameActive = true;

        // 코루틴 : 게임이 병렬적으로 여러가지 스크립트를 수행하는 듯한 효과를 줌
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
            // spawnRate 만큼 쉬었다가 코드를 수행 -> 이것을 계속 반복하여 병렬적으로 수행하듯이  됨
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]); // index 번호의 객체 생성
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // target에서 접근할 수 있도록 public으로 선언
    public void UpdateScore(int scoreToAdd) // 함수 인자는 얼만큼 더할지
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
}
