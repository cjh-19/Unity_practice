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
            // Destroy는 유니티에 게임오프젝트를 제거하고 삭제하는 명령어
            // gameObject : 현재 게임오프젝트 (c++의 this 포인터와 같은 것)
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
