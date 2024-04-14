using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StaticMeshGen))]

public class StaticMeshGenEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        StaticMeshGen script = (StaticMeshGen)target;

        if (GUILayout.Button("Generate Mesh"))
        {
            script.StartMesh();
        }
    }
}

public class StaticMeshGen : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartMesh()
    {
        Mesh mesh = new Mesh(); //Mesh : 3D��ü

        Vector3[] vretices = new Vector3[] //�ﰢ��
        {
            new Vector3(0.0f, 0.0f, 0.0f),
            new Vector3(1.0f, 0.0f, 0.0f),
            new Vector3(1.0f, 1.0f, 0.0f),
        };

        Vector3[] normal = new Vector3[] //�ﰢ��(normal)
        {
            new Vector3(0.0f, 0.0f, 0.0f),
            new Vector3(0.0f, 0.0f, 0.0f),
            new Vector3(0.0f, 0.0f, 0.0f),
        };

        mesh.vertices = vretices; //�� �ֱ�

        int[] trianglelndices = new int[]
        {
            //�޼����� ���ε��� �׸�
            //��� ���
            0,2,1,
        };

        mesh.triangles = trianglelndices;

        MeshFilter mf = this.AddComponent<MeshFilter>();
        MeshRenderer mr = this.AddComponent<MeshRenderer>();

        mf.mesh = mesh; //���� mesh �ֱ�
    }
    //Mesh Filter : �޽� ���Ͱ� �����ϴ� �޽�
    //Mesh Renderer : 

    // Update is called once per frame
    void Update()
    {
        
    }
}
