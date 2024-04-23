using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 3; // ���� ���
    private int currentHealth; // ���� ���

    void Start()
    {
        currentHealth = startingHealth; // ���� ����� ���� ������� �ʱ�ȭ
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Villain")) // �ٸ� ������Ʈ�� "Enemy" �±׸� ������ �ִ��� Ȯ��
        {
            TakeDamage(); // �浹 �� �÷��̾��� ��� ���� �Լ� ȣ��
        }
    }

    void TakeDamage()
    {
        currentHealth--; // �÷��̾��� ��� ����
        Debug.Log("Player health: " + currentHealth); // ���� ����� �α׿� ���

        if (currentHealth <= 0)
        {
            Die(); // ���� ����� 0 �����̸� Die �Լ� ȣ��
        }
    }

    void Die()
    {
        Debug.Log("Player died!");
        // ���⿡ �÷��̾� ����� ���� ó���� �߰��� �� �ֽ��ϴ�. ���� ��� ���� ���� ȭ�� ǥ�� ��.
    }
}
