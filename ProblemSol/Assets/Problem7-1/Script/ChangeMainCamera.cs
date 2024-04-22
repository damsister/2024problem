using Unity.VisualScripting;
using UnityEngine;

public class ChangeMainCamera : MonoBehaviour
{
    private Camera thisCamera;
    public GameObject Villain;

    private void Start()
    {
        thisCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (thisCamera == null)
        {
            Debug.Log("카메라 컴포넌트를 찾을 수 없습니다.");
            return;
        }

        // 카메라의 프로스텀을 가져오기
        FrustumPlanes frustum = new FrustumPlanes(thisCamera);

        // 모든 씬에 있는 모든 Villain 가져오기
        Renderer[] villains = FindObjectsOfType<Renderer>();

        foreach (Renderer Villain in villains)
        {
            // Villain의 중심점이 프러스텀 내에 있는지 확인
            bool isInsideFrustum = frustum.IsInsideFrustum(Villain.bounds.center);

            if (Villain.CompareTag("ground"))
            {
                this.gameObject.SetActive(true);
                continue;
            }

            if (isInsideFrustum && !Villain.gameObject.activeSelf) // 프러스텀 내에 있고 활성화되지 않았을 때
            {
                Villain.gameObject.SetActive(true); // 활성화
            }
            else if (!isInsideFrustum && Villain.gameObject.activeSelf) // 프러스텀 밖에 있고 활성화되어 있을 때
            {
                Villain.gameObject.SetActive(false); // 비활성화
            }
        }
    }

    // 프러스텀 플레인 클래스
    public class FrustumPlanes
    {
        private readonly Plane[] planes;

        public FrustumPlanes(Camera camera)
        {
            planes = GeometryUtility.CalculateFrustumPlanes(camera);
        }

        public bool IsInsideFrustum(Vector3 point)
        {
            foreach (var plane in planes)
            {
                if (!plane.GetSide(point)) // GetSide : 평면 안에 있는지 없는지 판단
                {
                    return false;
                }
            }
            return true;
        }
    }
}
