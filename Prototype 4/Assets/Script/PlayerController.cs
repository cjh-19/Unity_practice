using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb; // ��ü�� ��ü�� ������ ��Ʈ��
    private GameObject focalPoint; // focalPoint ��ü�� ������
    public PowerUpType currentPowerUp = PowerUpType.None;

    public GameObject rocketPrefab;
    private GameObject tmpRocket;
    private Coroutine powerupCountdown;

    public float speed = 5.0f;
    public bool hasPowerup = false; // �÷��̾ powerup �������� ���� �������� ����

    private float powerupStrength = 15.0f;

    public GameObject powerupIndicator;

    public float hangTime;
    public float smashSpeed;
    public float explosionForce;
    public float explosionRadius;

    bool smashing = false;
    float floorY;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>(); // ������Ʈ�� ������
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical"); // ���� �Է°�
        // focalPoint�� ������ ���Ͽ� �յڷ� �����̰� �Ѵ�.
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput); // ���� �յڷ� �̵��ϰ� ��
        powerupIndicator.transform.position = transform.position + new Vector3(0, 1.5f, 0); // powerup indicator�� �÷��̾� �Ӹ����� �ֵ���
    
        if(currentPowerUp == PowerUpType.Rockets && Input.GetKeyDown(KeyCode.F))
        {
            LaunchRockets();
        }

        if(currentPowerUp == PowerUpType.Smash && Input.GetKeyDown(KeyCode.Space) && !smashing)
        {
            smashing = true;
            StartCoroutine(Smash());
        }
    }

    // Ʈ���Ű� �߻����� �� powerup �±׸� ���� ��ü���� �˻�
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            currentPowerUp = other.gameObject.GetComponent<PowerUp>().powerupType;
            Destroy(other.gameObject); // powerup ��ü ����

            powerupIndicator.gameObject.SetActive(true);

            if(powerupCountdown != null)
            {
                StopCoroutine(powerupCountdown);
            }

            // �Ͻ������θ� powerupȿ���� �����
            powerupCountdown = StartCoroutine(PowerupCountdownRoutine());
            // Coroutine�� �������� ȿ���� �������� �Ѱ��� ����
            // �߰��߰� yield return�� �����ν� �� �Լ���
            // �ѹ��� ������ ������ ���� �ƴ�
            // ���� �����ӿ� ���� ����ǰ� �ִ� ������ ����
        }
    }

    // �ݵ�� IEnumerator ���̾���Ѵ�
    IEnumerator PowerupCountdownRoutine()
    {
        // �ѹ� ����ǰ� ������ ���� �ƴ� �߰��� ����Ƽ�� ������� �纸�Ѵ�
        // �׸��� 7�� �ڿ� �ٽ� ������� �����´�.
        // 7�� �ڸ� powerup ȿ���� �Ұ� �ȴ�.
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        currentPowerUp = PowerUpType.None;
        powerupIndicator.gameObject.SetActive(false);
    }

    IEnumerator Smash()
    {
        var enemies = FindObjectsOfType<Enemy>();

        floorY = transform.position.y;

        float jumpTime = Time.time + hangTime;
        while (Time.time < jumpTime)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, smashSpeed);
            yield return null;
        }
        while (transform.position.y > floorY)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, -smashSpeed * 2);
            yield return null;
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce,
                transform.position, explosionRadius, 0.0f, ForceMode.Impulse);
            }
        }

        smashing = false;
    }

    // enemy �� �ε������� ���� ����
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && currentPowerUp == PowerUpType.Pushback)
        {
            // ������ ū ���� ����
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            // �÷��̾�� �ε����� �� �� �ݴ� ����
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            enemyRigidbody.AddForce(awayFromPlayer* powerupStrength, ForceMode.Impulse); // ù��° ���ڴ� �����̴�.
            // �浹�� ����� �̸��� �������� powerup�� ��Ȯ�ϰ� ���õǾ� ������ �˷��ִ� �α�
            Debug.Log("Player cllided with " + collision.gameObject.name + " with powerup set to " + currentPowerUp.ToString());
        }
    }

    void LaunchRockets()
    {
        foreach(var enemy in FindObjectsOfType<Enemy>())
        {
            tmpRocket = Instantiate(rocketPrefab, transform.position + Vector3.up, Quaternion.identity);
            tmpRocket.GetComponent<RocketBehaviour>().Fire(enemy.transform);
        }
    }
}
