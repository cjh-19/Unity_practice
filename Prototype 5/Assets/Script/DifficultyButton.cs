using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    public int difficulty;
    private Button button;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // 버튼 컴포넌트를 가져옴
        button = GetComponent<Button>();
        // 눌렸을 때 이벤트 실행되는 핸들러
        button.onClick.AddListener(SetDifficulty);

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void SetDifficulty()
    {
        // 난이도 조절 인자를 받음
        gameManager.StartGame(difficulty);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
