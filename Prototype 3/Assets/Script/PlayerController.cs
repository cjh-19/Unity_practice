using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce; // ���� ��ġ ����
    public float gravityModifier; // �߷¿� ���� ������ ������ �� �ִ� ����
    public bool isOnGround = true; // ĳ���Ͱ� ���� �پ��ִ��� ������ �������� �ľ�
    // true�� ���� �ִ� ����

    // Start is called before the first frame update
    void Start()
    {
        // GetComponent ������ �ٵ� �����´µ� ������ ���ø� �Լ���
        // Ÿ���� ���ָ� �ش� Ÿ���� ������Ʈ�� ��ȯ���ش�
        // ���� �߰��Ǿ����� ���� ������Ʈ�� �ִ´ٸ� none�� ��ȯ�� ���̴�.
        playerRb = GetComponent<Rigidbody>();

        // physics ��⿡�� �߷°��� �ٲ� �� ���
        // gracityModifier�� 0���� �����ϸ� ���߷��� �ǹǷ� 1�̶�� ��ġ�� ����
        Physics.gravity *= gravityModifier;

    }

    // Update is called once per frame
    void Update()
    {
        // �����̽��ٸ� ������ �� if�� ����
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround) // ���� ������ �����̽��ٸ� ������ �� ����
        {
            // AddForce�Լ��� �����Ͽ� �� ĳ���͸� Ư���������� ���� ���ؼ� �����̰� �Ѵ�.
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    // �� �Լ��� ��ü�� �ٸ� ��ü�� �ε����� ���� ������
    {
        isOnGround = true;
    }
}
