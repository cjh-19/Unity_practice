using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // �±װ� �÷��̾��� life�� ����
        if (other.CompareTag("Player"))
        {
            gameManager.AddLive(-1);
            Destroy(gameObject);
        }
        // �±װ� �������� Ȯ���ϰ�, ���� �׷��ٸ� ������ �߰�
        else if (other.CompareTag("Animal") && gameObject.CompareTag("Food"))
        {
            other.GetComponent<AnimalHunger>().FeedAnimal(1); // AnimalHunger��ũ��Ʈ�� ���� ������ ������ ĥ ���� �浹�� ����
            Destroy(gameObject); // �ڽ� ���� - ��������
        }
    }
}
