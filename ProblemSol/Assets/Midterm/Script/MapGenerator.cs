using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapGenerator : MonoBehaviour
{
    public int mapWidth; // ���� ���� ũ��
    public int mapHeight; // ���� ���� ũ��
    public GameObject floorObject; // �� �ٴ��� ��Ÿ���� ������Ʈ

    public GameObject lowWallPrefab; // ���� �� ������
    public GameObject highWallPrefab; // ���� �� ������
    public GameObject pathPrefab; // ��� ������

    // CSV ������ ���
    public string mapDataPath = "Assets/Midterm/MapDataPath.csv";

    void Start()
    {
        GenerateMap(); // �� ���� �Լ� ȣ��
        GenerateMazeFromCSV(); // CSV ���Ϸκ��� �̷� ���� �Լ� ȣ��
    }

    void GenerateMap()
    {
        // Plane�� ũ�⸦ ���� ũ�⿡ �°� ����
        Transform planeTransform = GetComponentInChildren<Transform>(); // �ڽ� ������Ʈ �� ù ��°�� �߰ߵ� Transform�� ������
        planeTransform.localScale = new Vector3(mapWidth, 1, mapHeight); // ����, ���� ũ�� ����
    }

    void GenerateMazeFromCSV()
    {
        string[] lines = File.ReadAllLines(mapDataPath);

        // �ٴ� ����� ���� ��ġ ���
        float startX = -(mapWidth / 2.0f) + 0.5f;
        float startZ = -(mapHeight / 2.0f) + 0.5f;

        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            string[] cells = line.Split(',');

            for (int x = 0; x < cells.Length; x++)
            {
                int cellValue = int.Parse(cells[x]);

                // ���� ��ġ�� �ʴ��� Ȯ��
                bool shouldInstantiateWall = true;
                Collider[] colliders = Physics.OverlapSphere(new Vector3(startX + x, 0, startZ - y), 0.1f);
                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag("Wall"))
                    {
                        shouldInstantiateWall = false;
                        break;
                    }
                }

                // ���� ����
                if (shouldInstantiateWall)
                {
                    // ���� ���� ���
                    if (cellValue == 1)
                    {
                        Instantiate(lowWallPrefab, new Vector3(startX + x, 2f, startZ - y), Quaternion.identity);
                    }
                    // ���� ���� ���
                    else if (cellValue == 2)
                    {
                        Instantiate(highWallPrefab, new Vector3(startX + x, 1f, startZ - y), Quaternion.identity);
                    }
                    // ����� ���
                    else if (cellValue == 0)
                    {
                        Instantiate(pathPrefab, new Vector3(startX + x, 0, startZ - y), Quaternion.identity);
                    }
                }
            }
        }
    }
}
