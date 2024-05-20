using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]

public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MapGenerator script = (MapGenerator)target;

        if (GUILayout.Button("Generate Mesh"))
        {
            script.Start();
        }
    }
}

public class MapGenerator : MonoBehaviour
{
    public GameObject wallPrefab; // �� ������
    public TextAsset mapCSV; // �� ������ ���� CSV ����
    public Vector2 cellSize; // ���� ũ��
    public Transform planeTransform; // Plane�� Transform

    private int[,] mapData; // �� ������ ������ �迭
    private Vector2Int mapSize = new Vector2Int(10, 10); // ������ ���� ���μ��� ũ��

    public void Start()
    {
        // CSV ������ �о�� �� �����͸� �ʱ�ȭ
        LoadMapFromCSV();

        // �� ����
        GenerateMap();
    }

    void LoadMapFromCSV()
    {
        string[] lines = mapCSV.text.Split('\n');

        // �� ������ �迭 ����
        mapData = new int[mapSize.y, mapSize.x];

        // CSV ���Ͽ��� �� �����͸� �о�� �迭�� ����
        for (int i = 0; i < mapSize.y; i++)
        {
            string[] row = lines[i].Trim().Split(',');
            for (int j = 0; j < mapSize.x; j++)
            {
                mapData[i, j] = int.Parse(row[j]);
            }
        }
    }

    void GenerateMap()
    {
        // Plane�� ũ�⸦ ������
        Vector3 planeScale = planeTransform.localScale;

        // �̷��� ���� ��ġ ���
        Vector3 mazeStartPosition = new Vector3(-mapSize.x * cellSize.x / 2f + cellSize.x / 2f, 0f, mapSize.y * cellSize.y / 2f - cellSize.y / 2f);

        // �� �����͸� ������� ���� ���� �����Ͽ� ��ġ
        for (int i = 0; i < mapSize.y; i++)
        {
            for (int j = 0; j < mapSize.x; j++)
            {
                // ���� ��ġ�� �� ������ Ȯ��
                int cellType = mapData[i, j];

                // �� �Ǵ� ���� �����Ͽ� ��ġ
                if (cellType == 1 || cellType == 2 || cellType == 3)
                {
                    // ��ġ ���
                    Vector3 position = mazeStartPosition + new Vector3(j * cellSize.x, 0f, -i * cellSize.y);

                    if (cellType == 1 || cellType == 2)
                    {
                        // �� ����
                        GameObject wall = Instantiate(wallPrefab, position, Quaternion.identity);
                        wall.transform.localScale = new Vector3(cellSize.x, cellType == 2 ? 1.3f * cellSize.y : cellSize.y, cellSize.y);
                        wall.transform.parent = planeTransform;
                    }
                }
            }
        }
    }
}

