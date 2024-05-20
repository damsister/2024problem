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
            // 열쇠를 플레이어의 자식으로 설정
            this.transform.SetParent(other.transform);

            // 열쇠의 위치를 플레이어의 옆으로 이동 (예: 오른쪽으로 이동)
            this.transform.localPosition = new Vector3(1.0f, 0, 0);
        }
        else if(other.CompareTag("Door"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
