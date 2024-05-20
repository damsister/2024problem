using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            // ���踦 �÷��̾��� �ڽ����� ����
            this.transform.SetParent(other.transform);

            // ������ ��ġ�� �÷��̾��� ������ �̵� (��: ���������� �̵�)
            this.transform.localPosition = new Vector3(1.0f, 0, 0);
        }
        else if(other.CompareTag("Door"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
