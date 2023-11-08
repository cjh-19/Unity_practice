using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 파티클효과를 가져올 수 있는 변수
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtyParticle;

    public AudioClip jumpSound;
    public AudioClip crashSound; // 충돌했을 때의 사운드
    private AudioSource playerAudio;

    private Rigidbody playerRb;
    // Animator를 직접 건드려야 하므로 컴포넌트를 가져옴
    private Animator playerAnim;

    public float jumpForce; // 점프 수치 조절
    public float gravityModifier; // 중력에 대한 영향을 수정할 수 있는 변수
    public bool isOnGround = true; // 캐릭터가 땅에 붙어있는지 점프한 상태인지 파악
    // true는 땅에 있는 상태
    public bool gameOver = false; // 게임이 종료된다는 상태를 표시하는 변수
    public bool gameClear = false; // 게임이 클리어 됐다는 상태

    public bool doubleJumpUsed = false;
    public float doubleJumpForce;

    public bool tripleJumpUsed = false;
    public float tripleJumpForce;

    public bool doubleSpeed = false;

    // Start is called before the first frame update
    void Start()
    {
        // GetComponent 리지드 바디를 가져온는데 일종의 템플릿 함수로
        // 타입을 써주면 해당 타입의 컴포넌트를 반환해준다
        // 만약 추가되어있지 않은 컴포넌트를 넣는다면 none을 반환할 것이다.
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        // physics 모듈에서 중력값을 바꿀 때 사용
        // gracityModifier을 0으로 설정하면 무중력이 되므로 1이라는 수치로 설정
        Physics.gravity *= gravityModifier;

    }

    // Update is called once per frame
    void Update()
    {
        // 스페이스바를 눌렀을 때 if문 실행
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        // 게임이 끝나지 않고 땅에 있으며 스페이스바를 눌렀을 때 점프
        {
            // AddForce함수를 적용하여 이 캐릭터를 특정방향으로 힘을 가해서 움직이게 한다.
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;

            // 점프하는 애니메이션으로 바뀌게한다
            playerAnim.SetTrigger("Jump_trig");

            dirtyParticle.Stop();

            playerAudio.PlayOneShot(jumpSound, 1.0f);

            doubleJumpUsed = false;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && !isOnGround && !doubleJumpUsed)
        {
            doubleJumpUsed = true;
            playerRb.AddForce(Vector3.up*doubleJumpForce, ForceMode.Impulse);
            playerAnim.Play("Running_Jump", 3, 0f);
            playerAudio.PlayOneShot(jumpSound, 1.0f);

            tripleJumpUsed = false;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && !isOnGround & !tripleJumpUsed)
        {
            tripleJumpUsed = true;
            playerRb.AddForce(Vector3.up * tripleJumpForce, ForceMode.Impulse);
            playerAnim.Play("Running_Jump", 3, 0f);
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }


        if (Input.GetKey(KeyCode.LeftShift))
        {
            doubleSpeed = true;
            playerAnim.SetFloat("Speed_Multiplier", 2.0f);
        }
        else if (doubleSpeed)
        {
            doubleSpeed = false;
            playerAnim.SetFloat("Speed_Multiplier", 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    // 이 함수는 객체가 다른 물체와 부딪혔을 때를 감지함
    {
        // 땅에 닿아있으면 점프를 할 수 있음
        // 장애물과 부딪히면 게임 종료
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;

            dirtyParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle") && !gameClear)
        {
            gameOver = true;
            Debug.Log("Game Over!");
            // 죽는 애니메이션
            playerAnim.SetBool("Death_b", true); // 죽음
            playerAnim.SetInteger("DeathType_int", 1); // 죽는 타입은 1번

            explosionParticle.Play();
            dirtyParticle.Stop();

            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}
