using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutofBounds : MonoBehaviour
{
    private float topBound = 30;
    private float lowerBound = -10;
    private float sideBound = 30;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z > topBound)
        {
            // Destroy�� ����Ƽ�� ���ӿ�����Ʈ�� �����ϰ� �����ϴ� ��ɾ�
            // gameObject : ���� ���ӿ�����Ʈ (c++�� this �����Ϳ� ���� ��)
            Destroy(gameObject);
        }
        else if (transform.position.z < lowerBound)
        {
            //gameManager.AddLive(-1);
            Destroy(gameObject);
        }
        else if(transform.position.x > sideBound)
        {
            //gameManager.AddLive(-1);
            Destroy(gameObject);
        }
        else if(transform.position.x < -sideBound)
        {
            //gameManager.AddLive(-1);
            Destroy(gameObject);
        }
    }
}
