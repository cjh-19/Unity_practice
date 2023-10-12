using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Text �������� ����

public class GameManager : MonoBehaviour
{
    private int score = 0;
    private int lives = 5;
    private float GameTime = 60;
    public Text GameScoreText; // ȭ�鿡 ���ھ� ����ϱ� ���� ���� ����
    public Text GameLifeText;
    public Text GameTimeText;
    public GameObject Win_img;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if((int)GameTime == 0 || lives <= 0)
        {
            Debug.Log("���� ����");
            GameTimeText.text = "���� ����";
            GameLifeText.text = "Life: " + lives;
            GameScoreText.text = "Score: " + score;
        }
        else if(score >= 15)
        {
            Win_img.SetActive(true);
            GameLifeText.text = "Life: " + lives;
            GameScoreText.text = "Score: " + score;
        }
        else
        {
            GameTime -= Time.deltaTime;
            GameTimeText.text = "Time: " + (int)GameTime;
            GameLifeText.text = "Life: " + lives;
            GameScoreText.text = "Score: " + score;

        }
    }

    public void AddLive(int value)
    {
        lives += value;

        if (lives <= 0)
        {
            Debug.Log("Game Over");
            lives = 0;
        }
        Debug.Log("Lives = " + lives);
    }

    public void AddScore(int value)
    {
        score += value;
        Debug.Log("Score = " + score);
    }
}
