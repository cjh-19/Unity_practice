using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����� ������ �ݺ��ϰ� �ϴ� ��ũ��Ʈ
public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos; // ������ġ ����
    private float repeatWidth; // �ݺ��ؾ��ϴ� �ʺ��� ������ �̸� ����ؼ� �����ϴ� ����

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        // �ڽ��ö��̴��� �ʺ� ����ũ�⸸ŭ ������.
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ��ġ���� �������� �̵�(��ü ��� �ʺ��� ���ݸ�ŭ �����ߴٸ�) ����ġ
        // ũ�⸦ �˱����ؼ� �ݵ�� box collider�� �߰��ؼ� �˾ƾ��Ѵ�
        if(transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}
