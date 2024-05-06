using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Floor : MonoBehaviour
{
    public Transform startLocation; // ���� ��ġ
    public Transform escapeLocation; // Ż�� ��ġ

    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾ ���� ��ġ�� �����ߴ��� Ȯ��
        if (other.CompareTag("Player") && other.transform.position == startLocation.position)
        {
            // �÷��̾ ���� ��ġ�� �������� �� ó���� ���� �ۼ�
            Debug.Log("Player reached the starting point.");
        }

        // �÷��̾ Ż�� ��ġ�� �����ߴ��� Ȯ��
        if (other.CompareTag("Player") && other.transform.position == escapeLocation.position)
        {
            // Ż�� ��ġ�� �������� �� ���� Ŭ���� ó��
            Debug.Log("Player reached the escape point. Game cleared!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // ���� �� �ٽ� �ε� (���� ����)
        }
    }
}
