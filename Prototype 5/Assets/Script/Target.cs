using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int pointValue;
    // ��ƼŬ ȿ��
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
        // ���ӸŴ������� ���ӸŴ��� ������Ʈ�� ������
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        targetRb = GetComponent<Rigidbody>();
        // �� �������� ���� ����. 12���� 16����
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        // AddTorque�� ȸ���ϴµ� ���� ��. x,y,z �������� ȸ������ ����.
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        // �ʱ� ��ġ ���� x�� -10���� 10, y�� -6, z�� 0����.
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

    // ��ü�� ���콺�� Ŭ���ϸ� ������� ��
    private void OnMouseDown()
    {
        Destroy(gameObject);
        // ��ǥ���� ���� ��ġ���� ���۵ǵ��� transform.position
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        gameManager.UpdateScore(pointValue);
    }

    // Ʈ���Ű� �Ͼ�� ���� ����
    // ȭ�� �Ʒ������� �������� �� ����
    // ������� ��ü�� Ʈ���� ������ ��
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
