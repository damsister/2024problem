using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR //������
using UnityEditor;
#endif
//#if ~ #endif : ��ó���� ���ù�

public class villain : MonoBehaviour
{
    public GameObject villains;
    public int ObjectNumber = 0;
    public float spawnInterval = 10.0f; // �Ǵ� ���� ����
    public Vector3 minBounds;
    public Vector3 maxBounds;

    /*#if UNITY_EDITOR //������ ���� ��
        [CustomEditor(typeof(villain))]
        public class villainGeneratorEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                villain v = (villain)target;

                // �Ǵ� ���� ������ ������ �� �ִ� �ʵ带 �߰��մϴ�.
                v.spawnInterval = EditorGUILayout.FloatField("�Ǵ� ���� ����", v.spawnInterval);

                // �Ǵ� ���� ��ư ��� �����մϴ�.
                // if(GUILayout.Button("villain Generate"))
                // {
                //     v.GenerateObject();
                // }
            }
        }
    #endif*/

    //Plane�� ���� ������ �� ������ ��ġ�� ����
    // �� ���� Object�� �����ϰ� ��ġ�ϴ� �ڵ带 �ۼ��ϼ���.
    public void GenerateObject()
    {
        // �� ���� Object�� �����ϰ� ��ġ�ϴ� �ڵ带 �ۼ��ϼ���.
        for (int i = 0; i < ObjectNumber; i++)
        {
            // ������ ���� ������ ���� ��ġ�� �����մϴ�.
            Vector3 randomPosition = new Vector3(
                Random.Range(minBounds.x, maxBounds.x),
                Random.Range(minBounds.y, maxBounds.y),
                Random.Range(minBounds.z, maxBounds.z)
            );

            // TargetObject�� �����ϰ� ������ ��ġ�� ��ġ�մϴ�.
            GameObject newObject = Instantiate(villains, randomPosition, Quaternion.identity);
        }

        /* // Plane�� �ĺ��ϰ� �ش� ����� ���� ������ ���� ��ġ�� �����մϴ�.
         GameObject plane = GameObject.FindGameObjectWithTag("plane");
         if (plane != null)
         {
             Renderer planeRenderer = plane.GetComponent<Renderer>();

             for (int i = 0; i < ObjectNumber; i++)
             {
                 Vector3 randomPosition = new Vector3(
                     Random.Range(planeRenderer.bounds.min.x, planeRenderer.bounds.max.x),
                     Random.Range(planeRenderer.bounds.min.y, planeRenderer.bounds.max.y),
                     Random.Range(planeRenderer.bounds.min.z, planeRenderer.bounds.max.z)
                 );

                 // TargetObject�� �����ϰ� ������ ��ġ�� ��ġ�մϴ�.
                 GameObject newObject = Instantiate(villains, randomPosition, Quaternion.identity);
             }
         }
         else
         {
             Debug.Log("Plane not found. Cannot generate objects.");
         }*/
    }

    private void Start()
    {
        // ������ �������� GenerateObject �Լ��� ȣ���ϴ� �ڷ�ƾ ����
        StartCoroutine(SpawnVillains());
    }

    private IEnumerator SpawnVillains()
    {
        while (true)
        {
            GenerateObject(); // �Ǵ� ���� �Լ� ȣ��

            yield return new WaitForSeconds(spawnInterval); // ������ �ð� ������ ��ٸ�
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected() //GizomsSelected() //�������̵���
    {
        // Box ������ ���̵������ �׸��ϴ�.
        Handles.color = Color.yellow;

        Matrix4x4 cubeTransform = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
        //Matrix4x4 : 4*4 ���
        //TRS(Trans, Rot, Scal) : �̵����, ȸ�����, ũ�⺯ȯ���

        using (new Handles.DrawingScope(cubeTransform)) //cubeTransfrom�� �׸� �� �����Ѵ�. //using�� transfrom�� �÷��ּ���
        {
            Handles.DrawWireCube(Vector3.zero, Vector3.one);
        }
    }
#endif
}
//�ڷᱸ�� : tree