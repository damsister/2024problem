using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 3f; // �þ߰Ÿ� (���� ĸ�� �밢�� ������ 3��)
    public float viewAngle = 45f; // �þ߰�
    public float moveSpeed = 2f; // �̵� �ӵ�

    private Vector3 originalPosition; // �ʱ� ��ġ
    private Quaternion originalRotation; // �ʱ� ȸ��
    private bool isChasing = false; // �÷��̾� ���� ������ ����
    private bool isReturning = false; // �ʱ� ��ġ�� ���� ������ ����
    private Vector3[] patrolPoints; // ���� ����
    private int currentPatrolIndex = 0; // ���� ���� ���� �ε���
    private float returnTimer = 0f; // �ʱ� ��ġ ���� Ÿ�̸�
    private const float returnDuration = 6f; // �ʱ� ��ġ ���� ���� �ð�

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        patrolPoints = new Vector3[]
        {
            originalPosition + Quaternion.Euler(0f, -90f, 0f) * transform.forward * detectionRange,
            originalPosition + Quaternion.Euler(0f, 180f, 0f) * transform.forward * detectionRange
        };
    }

    void Update()
    {
        // �÷��̾ �þ� ���� ������ ����
        if (CanSeePlayer())
        {
            isChasing = true;
            isReturning = false;
            returnTimer = 0f;
            ChasePlayer();
        }
        else
        {
            // �÷��̾ ������ ����� �ʱ� ��ġ�� ����
            if (!isReturning)
            {
                isReturning = true;
                ReturnToOriginalPosition();
            }
            else
            {
                // �ʱ� ��ġ�� �������� ��, �ٽ� ���� ����
                if (Vector3.Distance(transform.position, originalPosition) < 0.1f)
                {
                    isReturning = false;
                    isChasing = false;
                    transform.rotation = originalRotation;
                }
            }
        }
    }

    // �÷��̾ �þ� ���� �ִ��� Ȯ��
    bool CanSeePlayer()
    {
        if (player == null)
            return false;

        Vector3 direction = player.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);

        // �÷��̾ �þ߰� ���� �ְ� �þ߰Ÿ� �̳��� ������ true ��ȯ
        if (angle < viewAngle / 2f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction.normalized, out hit, detectionRange))
            {
                if (hit.collider.gameObject == player.gameObject)
                {
                    return true;
                }
            }
        }

        return false;
    }

    // �÷��̾� ����
    void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    // �ʱ� ��ġ�� ����
    void ReturnToOriginalPosition()
    {
        if (returnTimer < returnDuration)
        {
            // 6�� ���� 90�� �������� ȸ��
            if (returnTimer < returnDuration / 2f)
            {
                transform.Rotate(0f, -90f * Time.deltaTime / (returnDuration / 2f), 0f);
            }
            // ���� 6�� ���� 180�� �������� ȸ��
            else
            {
                transform.Rotate(0f, 180f * Time.deltaTime / (returnDuration / 2f), 0f);
            }
            returnTimer += Time.deltaTime;
        }
        else
        {
            // ���� �������� �̵�
            Vector3 targetPosition = patrolPoints[currentPatrolIndex];
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(direction);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;

            // ���� ������ �����ϸ� ���� ���� �������� �̵�
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            }
        }
    }
}
