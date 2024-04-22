using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VillainControll : MonoBehaviour
{
    public float moveSpeed = 5f; // �Ǵ��� �̵� �ӵ�
    public float detectionRange = 10f; // �÷��̾ �����ϴ� ����

    private Transform player; // �÷��̾��� Transform ������Ʈ

    void Start()
    {
        // �÷��̾ ã�Ƽ� Transform�� ������
        player = GameObject.FindGameObjectWithTag("player").transform;
    }

    void Update()
    {
        // �÷��̾���� �Ÿ��� ���
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // �÷��̾ ���� ���� ���� �ִ��� Ȯ��
        if (distanceToPlayer <= detectionRange)
        {
            // �÷��̾ ���� �̵�
            transform.LookAt(player); // �÷��̾ �ٶ�
            transform.position += transform.forward * moveSpeed * Time.deltaTime; // ������ �̵�
        }
    }
}
