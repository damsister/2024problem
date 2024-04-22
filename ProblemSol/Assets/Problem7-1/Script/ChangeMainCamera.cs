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
            Debug.Log("ī�޶� ������Ʈ�� ã�� �� �����ϴ�.");
            return;
        }

        // ī�޶��� ���ν����� ��������
        FrustumPlanes frustum = new FrustumPlanes(thisCamera);

        // ��� ���� �ִ� ��� Villain ��������
        Renderer[] villains = FindObjectsOfType<Renderer>();

        foreach (Renderer Villain in villains)
        {
            // Villain�� �߽����� �������� ���� �ִ��� Ȯ��
            bool isInsideFrustum = frustum.IsInsideFrustum(Villain.bounds.center);

            if (Villain.CompareTag("ground"))
            {
                this.gameObject.SetActive(true);
                continue;
            }

            if (isInsideFrustum && !Villain.gameObject.activeSelf) // �������� ���� �ְ� Ȱ��ȭ���� �ʾ��� ��
            {
                Villain.gameObject.SetActive(true); // Ȱ��ȭ
            }
            else if (!isInsideFrustum && Villain.gameObject.activeSelf) // �������� �ۿ� �ְ� Ȱ��ȭ�Ǿ� ���� ��
            {
                Villain.gameObject.SetActive(false); // ��Ȱ��ȭ
            }
        }
    }

    // �������� �÷��� Ŭ����
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
                if (!plane.GetSide(point)) // GetSide : ��� �ȿ� �ִ��� ������ �Ǵ�
                {
                    return false;
                }
            }
            return true;
        }
    }
}
