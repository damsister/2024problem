using Unity.VisualScripting;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    public Material material1; // 프러스텀 내부에 있는 오브젝트에 적용될 머티리얼
    public Material material2; // 프러스텀 밖에 있는 오브젝트에 적용될 머티리얼

    private Camera thisCamera;

    private void Start()
    {
        thisCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (thisCamera == null)
        {
            Debug.LogError("카메라 컴포넌트를 찾을 수 없습니다.");
            return;
        }

        // 카메라의 프러스텀을 가져오기
        FrustumPlanes frustum = new FrustumPlanes(thisCamera);

        // 모든 씬에 있는 모든 Renderer 가져오기
        Renderer[] renderers = FindObjectsOfType<Renderer>();

        foreach (Renderer renderer in renderers)
        {

            // Renderer의 중심점이 프러스텀 내에 있는지 확인
            if (frustum.IsInsideFrustum(renderer.bounds.center))
            {
                // 프러스텀 내에 있는 경우 Material1 적용
                renderer.material = material1;
            }
            else
            {
                // 프러스텀 밖에 있는 경우 Material2 적용
                renderer.material = material2;
            }
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
            if (!plane.GetSide(point)) //GetSide : 평면 안에 있는지 없는지 판단
            {

                return false;
            }
        }
        return true;
    }
    //distance : 거리
    //아웃오브코어
    //쉐이더는 카툰, 퐁쉐이딩만 할 줄 알면됨
}
