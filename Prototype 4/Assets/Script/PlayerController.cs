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
