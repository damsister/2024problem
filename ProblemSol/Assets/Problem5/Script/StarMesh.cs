using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StarMesh))]

public class StarMeshEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        StarMesh script = (StarMesh)target;

        if (GUILayout.Button("Generate Mesh"))
        {
            script.StartMesh();
        }
    }
}

public class StarMesh : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartMesh()
    {
        Mesh mesh = new Mesh();

        // 정점들을 정의합니다.
        Vector3[] vertices = new Vector3[]
        {
            // 오각형
            new Vector3(0.0f, 0.0f, 0.0f),     // 꼭지점 0 (맨 아래 중앙)
            new Vector3(-0.7f, 0.5f, 0.0f),   // 꼭지점 1 (왼쪽 하단)
            new Vector3(-0.5f, 1.2f, 0.0f),  // 꼭지점 2 (왼쪽 중앙 위)
            new Vector3(0.5f, 1.2f, 0.0f), // 꼭지점 3 (오른쪽 중앙 위)
            new Vector3(0.7f, 0.5f, 0.0f),  // 꼭지점 4 (오른쪽 하단)
            // 삼각형
            new Vector3(-1.0f, -0.5f, 0.0f),     // 꼭지점 5 (왼쪽 하단)
            new Vector3(-1.5f, 1.2f, 0.0f),    // 꼭지점 6 (왼쪽 상단)
            new Vector3(0.0f, 1.9f, 0.0f),   // 꼭지점 7 (맨 위 중앙)
            new Vector3(1.5f, 1.2f, 0.0f),  // 꼭지점 8 (오른쪽 상단)
            new Vector3(1.0f, -0.5f, 0.0f),    // 꼭지점 9 (오른쪽 하단)
            // 사각형(기둥)1
            new Vector3(0.0f, 0.0f, 1.0f),    // 꼭지점 10 (사각형 기준 왼쪽 아래)
            new Vector3(-1.0f, -0.5f, 1.0f),    // 꼭지점 11 (사각형 기준 오른쪽 아래)
            // 사각형(기둥)2
            new Vector3(-0.5f, 0.5f, 1.0f),    // 꼭지점 12
            new Vector3(-1.0f, -0.5f, 1.0f),    // 꼭지점 13(11)
            // 사각형(기둥)3
            new Vector3(-0.5f, 0.5f, 1.0f),    // 꼭지점 14(12)
            new Vector3(-1.5f, 1.2f, 1.0f),    // 꼭지점 15
            // 사각형(기둥)4 
            new Vector3(-0.5f, 1.2f, 1.0f),    // 꼭지점 16
            new Vector3(-1.5f, 1.2f, 1.0f),    // 꼭지점 17(15)
            // 사각형(기둥)5 
            new Vector3(0.0f, 1.9f, 1.0f),    // 꼭지점 18
            new Vector3(-0.5f, 1.2f, 1.0f),    // 꼭지점 19(16)
            // 사각형(기둥)6
            new Vector3(0.5f, 1.2f, 1.0f),    // 꼭지점 20
            new Vector3(0.0f, 1.9f, 1.0f),    // 꼭지점 21(18)
            // 사각형(기둥)7
            new Vector3(1.5f, 1.2f, 1.0f),    // 꼭지점 22
            new Vector3(0.5f, 1.2f, 1.0f),    // 꼭지점 23(20)
            // 사각형(기둥)8
            new Vector3(0.7f, 0.5f, 1.0f),    // 꼭지점 24
            new Vector3(1.5f, 1.2f, 1.0f),    // 꼭지점 25(22)
            // 사각형(기둥)9
            new Vector3(1.0f, -0.5f, 1.0f),    // 꼭지점 26
            new Vector3(0.7f, 0.5f, 1.0f),    // 꼭지점 27(24)
            // 사각형(기둥)10
            new Vector3(1.0f, -0.5f, 1.0f),    // 꼭지점 28(26)
            new Vector3(0.0f, 0.0f, 1.0f),    // 꼭지점 29(0)

            //바닥면
            // 오각형
            new Vector3(0.0f, 0.0f, 1.0f),     // 꼭지점 30 (바닥면 중앙)
            new Vector3(-0.7f, 0.5f, 1.0f),   // 꼭지점 31 (바닥면 왼쪽 하단)
            new Vector3(-0.5f, 1.2f, 1.0f),  // 꼭지점 32 (바닥면 왼쪽 중앙 위)
            new Vector3(0.5f, 1.2f, 1.0f), // 꼭지점 33 (바닥면 오른쪽 중앙 위)
            new Vector3(0.7f, 0.5f, 1.0f),  // 꼭지점 34 (바닥면 오른쪽 하단)
            // 삼각형
            new Vector3(-1.0f, -0.5f, 1.0f),     // 꼭지점 35 (왼쪽 하단)
            new Vector3(-1.5f, 1.2f, 1.0f),    // 꼭지점 36 (왼쪽 상단)
            new Vector3(0.0f, 1.9f, 1.0f),   // 꼭지점 37 (맨 위 중앙)
            new Vector3(1.5f, 1.2f, 1.0f),  // 꼭지점 38 (오른쪽 상단)
            new Vector3(1.0f, -0.5f, 1.0f),    // 꼭지점 39 (오른쪽 하단)
        };

        // 삼각형 인덱스 배열을 정의합니다.
        int[] triangleIndices = new int[]
        {
            // 오각형
            0, 1, 2,
            0, 2, 3,
            0, 3, 4,
            // 삼각형
            0, 5, 1,
            1, 6, 2,
            2, 7, 3,
            3, 8, 4,
            4, 9, 0,
            // 사각형1
            0, 10, 11,
            0, 11, 5,
            //사각형2
            1, 12, 11,
            1, 11, 5,
            //사각형3
            1, 12, 15,
            1, 15, 6,
            //사각형4
            2, 16, 15,
            2, 15, 6,
            //사각형5
            7, 18, 16,
            7, 16, 2,
            //사각형6
            3, 20, 18,
            3, 18, 7,
            //사각형7
            8, 22, 20,
            8, 20, 3,
            //사각형8
            8, 22, 24,
            8, 24, 4,
            //사각형9
            9, 26, 24,
            9, 24, 4,
            //사각형10
            9, 26, 10,
            9, 10, 0,
            // 오각형2
            30, 31, 32,
            30, 32, 33,
            30, 33, 34,
            // 삼각형2
            30, 35, 31,
            31, 36, 32,
            32, 37, 33,
            33, 38, 34,
            34, 39, 30,
        };

        mesh.vertices = vertices;
        mesh.triangles = triangleIndices;

        MeshFilter mf = gameObject.GetComponent<MeshFilter>();
        if (mf == null)
            mf = gameObject.AddComponent<MeshFilter>();

        MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
        if (mr == null)
            mr = gameObject.AddComponent<MeshRenderer>();

        mf.mesh = mesh;
        mr.material.shader = Shader.Find("Custom/Yellow");
    }
}
