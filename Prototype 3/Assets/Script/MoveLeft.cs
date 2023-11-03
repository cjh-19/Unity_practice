using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 30; // �̸� ���ǵ� �ӵ��� �������� ��� �̵��ϰ� ��
    private PlayerController playerControllerScript; // �÷��̾� ��Ʈ�ѷ� ������Ʈ�� ����

    private float leftBound = -15;

    // Start is called before the first frame update
    void Start()
    {
        // �� ������ �ʱ�ȭ�� ���� ���� ����ȭ�� �ȿ� �ִ� Ư�� �̸��� �ִ� ������Ʈ�� �������� �Լ�
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // playercontrolller�� gameOver������ public�̹Ƿ� ���� ����
        // gameOver�� false�� ���� �����̰� �Ѵ�.
        if(playerControllerScript.gameOver == false) 
        {
            if (playerControllerScript.doubleSpeed)
            {
                transform.Translate(Vector3.left * Time.deltaTime * (speed * 2));
            }
            else {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
        }

        // ���ڰ� �ٴ����� �������� �����Ѵ�
        if(transform.position.x < leftBound && gameObject.CompareTag("Obstacle")) 
        {
            Destroy(gameObject);
        }
    }
}
