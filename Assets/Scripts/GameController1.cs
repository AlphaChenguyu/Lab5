using UnityEngine;

public class GameController1 : MonoBehaviour
{
    public GameObject asteroidPrefab;   // С����Ԥ����
    public GameObject gameAreaObject;   // ��ʾ��Ϸ����Ķ���һ�������壬����߽磩
    public int[,] m_Grid;                // ������������0 = �գ�1 = С���ǣ�

    public int T;                  // ϸ���Զ�����������
    public float asteroidSize;   // ÿ��С���ǵĴ�С�������������壩

    public float playableAreaWidth;
    private int gridWidth;              // �����ȣ�������
    private int gridHeight;             // ����߶ȣ�������

    public GameObject lastSpawnedAsteroid;
    void Start()
    {
        InitializeGameArea();
        InitializeGrid();
        RunCellularAutomata();
        SpawnAsteroids();
    }

    void InitializeGameArea()
    {
        // ��ȡ��Ϸ�����С�����ڴ�������������
        float gameAreaWidth = gameAreaObject.transform.localScale.x;
        float gameAreaHeight = gameAreaObject.transform.localScale.z;

        // ���������Ⱥ͸߶�
        gridWidth = Mathf.FloorToInt(gameAreaWidth / asteroidSize);
        gridHeight = Mathf.FloorToInt(gameAreaHeight / asteroidSize);

        // ��ʼ������
        m_Grid = new int[gridWidth, gridHeight];
    }

    void InitializeGrid()
    {
        // �����ʼ������ԪΪ0��1
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                m_Grid[x, y] = Random.Range(0, 2);  // ���Ϊ0��1
            }
        }
    }

    void RunCellularAutomata()
    {
        for (int i = 0; i < T; i++)
        {
            int[,] newGrid = new int[gridWidth, gridHeight];

            for (int x = 0; x < gridWidth; x++)
            {
                for (int y = 0; y < gridHeight; y++)
                {
                    int neighbors = CountAliveNeighbors(x, y);

                    // Ӧ�ù���
                    if (m_Grid[x, y] == 1)
                    {
                        newGrid[x, y] = (neighbors < 2 || neighbors > 3) ? 0 : 1; // ϸ������
                    }
                    else
                    {
                        newGrid[x, y] = (neighbors == 3) ? 1 : 0; // ϸ������
                    }
                }
            }

            // ��������Ϊ��״̬
            m_Grid = newGrid;
            DebugGrid();
        }
    }

    int CountAliveNeighbors(int x, int y)
    {
        int count = 0;

        // ����8�����ܵ��ھ�
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0) continue; // ��������Ԫ

                int nx = x + dx;
                int ny = y + dy;

                // ���߽�
                if (nx >= 0 && nx < gridWidth && ny >= 0 && ny < gridHeight)
                {
                    count += m_Grid[nx, ny];
                }
            }
        }

        return count;
    }

    void SpawnAsteroids()
    {
            // ��ȡ��Ϸ����Ŀ��
            float halfWidth = playableAreaWidth / 2;

            // ���������ÿ����Ԫ��
            for (int x = 0; x < m_Grid.GetLength(0); x++)
            {
                for (int y = 0; y < m_Grid.GetLength(1); y++)
                {
                    // �������ֵΪ 1��������С����
                    if (m_Grid[x, y] == 1)
                    {
                        // ����С���ǵ� X λ��
                        // ����������ת��Ϊ��Ϸ����� X ��λ��
                        float spawnX = (x - (m_Grid.GetLength(0) / 2)) * (playableAreaWidth / m_Grid.GetLength(0));
                        // ����ѡ��һ���̶��� Z λ�ã����߸�����Ҫ���е���
                        float spawnZ = 0; // ������Ҫ���� Z ��λ��

                        // ��������λ��
                        Vector3 spawnPosition = new Vector3(spawnX, 0, spawnZ);
                        // ʵ����С����
                        Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
                    }
                }
            }
    }
    void DebugGrid()
    {
        for (int y = 0; y < gridHeight; y++)
        {
            string row = "";
            for (int x = 0; x < gridWidth; x++)
            {
                row += m_Grid[x, y] + " ";
            }
            Debug.Log(row);
        }
    }
}
