using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraControll : MonoBehaviour
{
    // 카메라가 추적할 대상 (플레이어)
    public Transform target;
    // 회전 속도
    public float rotationSpeed = 5f;

    void LateUpdate()
    {
        if (target == null)
            return;

        // 플레이어를 따라 이동
        transform.position = target.position;

        // 플레이어의 회전 각도를 카메라에 적용
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, rotationSpeed * Time.deltaTime);
    }
}
