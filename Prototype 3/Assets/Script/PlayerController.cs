using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ��ƼŬȿ���� ������ �� �ִ� ����
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtyParticle;

    public AudioClip jumpSound;
    public AudioClip crashSound; // �浹���� ���� ����
    private AudioSource playerAudio;

    private Rigidbody playerRb;
    // Animator�� ���� �ǵ���� �ϹǷ� ������Ʈ�� ������
    private Animator playerAnim;

    public float jumpForce; // ���� ��ġ ����
    public float gravityModifier; // �߷¿� ���� ������ ������ �� �ִ� ����
    public bool isOnGround = true; // ĳ���Ͱ� ���� �پ��ִ��� ������ �������� �ľ�
    // true�� ���� �ִ� ����
    public bool gameOver = false; // ������ ����ȴٴ� ���¸� ǥ���ϴ� ����
    public bool gameClear = false; // ������ Ŭ���� �ƴٴ� ����

    public bool doubleJumpUsed = false;
    public float doubleJumpForce;

    public bool tripleJumpUsed = false;
    public float tripleJumpForce;

    public bool doubleSpeed = false;

    // Start is called before the first frame update
    void Start()
    {
        // GetComponent ������ �ٵ� �����´µ� ������ ���ø� �Լ���
        // Ÿ���� ���ָ� �ش� Ÿ���� ������Ʈ�� ��ȯ���ش�
        // ���� �߰��Ǿ����� ���� ������Ʈ�� �ִ´ٸ� none�� ��ȯ�� ���̴�.
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        // physics ��⿡�� �߷°��� �ٲ� �� ���
        // gracityModifier�� 0���� �����ϸ� ���߷��� �ǹǷ� 1�̶�� ��ġ�� ����
        Physics.gravity *= gravityModifier;

    }

    // Update is called once per frame
    void Update()
    {
        // �����̽��ٸ� ������ �� if�� ����
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        // ������ ������ �ʰ� ���� ������ �����̽��ٸ� ������ �� ����
        {
            // AddForce�Լ��� �����Ͽ� �� ĳ���͸� Ư���������� ���� ���ؼ� �����̰� �Ѵ�.
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;

            // �����ϴ� �ִϸ��̼����� �ٲ���Ѵ�
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
    // �� �Լ��� ��ü�� �ٸ� ��ü�� �ε����� ���� ������
    {
        // ���� ��������� ������ �� �� ����
        // ��ֹ��� �ε����� ���� ����
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;

            dirtyParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle") && !gameClear)
        {
            gameOver = true;
            Debug.Log("Game Over!");
            // �״� �ִϸ��̼�
            playerAnim.SetBool("Death_b", true); // ����
            playerAnim.SetInteger("DeathType_int", 1); // �״� Ÿ���� 1��

            explosionParticle.Play();
            dirtyParticle.Stop();

            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}
