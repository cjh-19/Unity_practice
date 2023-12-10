using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int pointValue;
    // 파티클 효과
    public ParticleSystem explosionParticle;

    private GameManager gameManager;
    private Rigidbody targetRb;

    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -3;

    // Start is called before the first frame update
    void Start()
    {
        // 게임매니저에서 게임매니저 컴포넌트를 가져옴
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        targetRb = GetComponent<Rigidbody>();
        // 위 방향으로 힘을 가함. 12에서 16으로
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        // AddTorque는 회전하는데 들어가는 힘. x,y,z 방향으로 회적력을 가함.
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        // 초기 위치 설정 x는 -10에서 10, y는 -6, z는 0으로.
        transform.position = RandomSpawnPos();
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 객체를 마우스로 클릭하면 사라지게 함
    private void OnMouseDown()
    {
        Destroy(gameObject);
        // 목표물과 같은 위치에서 시작되도록 transform.position
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        gameManager.UpdateScore(pointValue);
    }

    // 트리거가 일어났을 때도 제거
    // 화면 아래쪽으로 내려갔을 때 제거
    // 센서라는 객체가 트리거 역할을 함
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
