using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapGenerator : MonoBehaviour
{
    public GameObject wallPrefab; // �� ������
    public GameObject doorPrefab; // �� ������
    public GameObject keyPrefab; // ���� ������
    public TextAsset mapCSV; // �� ������ ���� CSV ����
    public Vector2 cellSize; // ���� ũ��
    public Transform planeTransform; // Plane�� Transform

    private int[,] mapData; // �� ������ ������ �迭
    private Vector2Int mapSize = new Vector2Int(10, 10); // ������ ���� ���μ��� ũ��

    private bool hasKey = false; // ���踦 �������� ����

    void Start()
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

        // ������ ��ġ�� ���� ����
        for (int i = 0; i < mapSize.y; i++)
        {
            for (int j = 0; j < mapSize.x; j++)
            {
                if (mapData[i, j] == 4)
                {
                    // ��ġ ���
                    Vector3 position = mazeStartPosition + new Vector3(j * cellSize.x, 0f, -i * cellSize.y);

                    // ���� ����
                    GameObject key = Instantiate(keyPrefab, position + new Vector3(0f, 0.2f, 0f), Quaternion.identity);
                    key.transform.parent = planeTransform;
                }
            }
        }

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
                    else if (cellType == 3)
                    {
                        // �� ����
                        GameObject door = Instantiate(doorPrefab, position, Quaternion.identity);
                        // ���� 90���� ȸ����Ŵ
                        door.transform.Rotate(Vector3.up, 90f);

                        door.transform.localScale = new Vector3(cellSize.x, cellSize.y, 0.1f);
                        door.transform.parent = planeTransform;

                        // ���� ���踦 �Ҵ�
                        if (!hasKey && mapData[i, j] == 4)
                        {
                            GameObject key = Instantiate(keyPrefab, position + new Vector3(0f, cellSize.y / 2f, 0f), Quaternion.identity);
                            key.transform.parent = door.transform;
                            hasKey = true;
                        }
                    }
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾�� �浹�� ���
        {
            Destroy(gameObject); // ���� ������Ʈ �ı�
        }
    }
}

