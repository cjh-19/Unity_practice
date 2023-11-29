using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb; // 물체의 강체를 가져와 컨트롤
    private GameObject focalPoint; // focalPoint 객체를 가져옴
    public PowerUpType currentPowerUp = PowerUpType.None;

    public GameObject rocketPrefab;
    private GameObject tmpRocket;
    private Coroutine powerupCountdown;

    public float speed = 5.0f;
    public bool hasPowerup = false; // 플레이어가 powerup 아이템을 먹은 상태인지 구분

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
        playerRb = GetComponent<Rigidbody>(); // 컴포넌트를 가져옴
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical"); // 수직 입력값
        // focalPoint의 앞쪽을 향하여 앞뒤로 움직이게 한다.
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput); // 공을 앞뒤로 이동하게 함
        powerupIndicator.transform.position = transform.position + new Vector3(0, 1.5f, 0); // powerup indicator가 플레이어 머리위에 있도록
    
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

    // 트리거가 발생했을 때 powerup 태그를 가진 객체인지 검사
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            currentPowerUp = other.gameObject.GetComponent<PowerUp>().powerupType;
            Destroy(other.gameObject); // powerup 객체 삭제

            powerupIndicator.gameObject.SetActive(true);

            if(powerupCountdown != null)
            {
                StopCoroutine(powerupCountdown);
            }

            // 일시적으로만 powerup효과를 얻게함
            powerupCountdown = StartCoroutine(PowerupCountdownRoutine());
            // Coroutine는 병렬적인 효과를 내기위한 한가지 수단
            // 중간중간 yield return을 함으로써 이 함수가
            // 한번에 실행이 끝나는 것이 아닌
            // 여러 프레임에 거쳐 실행되고 있다 정도만 알자
        }
    }

    // 반드시 IEnumerator 형이어야한다
    IEnumerator PowerupCountdownRoutine()
    {
        // 한번 실행되고 끝나는 것이 아닌 중간에 유니티에 제어권을 양보한다
        // 그리고 7초 뒤에 다시 제어권을 가져온다.
        // 7초 뒤면 powerup 효과를 잃게 된다.
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

    // enemy 와 부딪혔는지 여부 검출
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && currentPowerUp == PowerUpType.Pushback)
        {
            // 적에게 큰 힘을 가함
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            // 플레이어와 부딪혔을 때 그 반대 방향
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            enemyRigidbody.AddForce(awayFromPlayer* powerupStrength, ForceMode.Impulse); // 첫번째 인자는 벡터이다.
            // 충돌한 대상의 이름이 가져오고 powerup이 정확하게 세팅되어 있음을 알려주는 로그
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
