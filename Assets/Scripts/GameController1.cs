using UnityEngine;

public class GameController1 : MonoBehaviour
{
    public GameObject asteroidPrefab;   // 小行星预制体
    public GameObject gameAreaObject;   // 表示游戏区域的对象（一个立方体，定义边界）
    public int[,] m_Grid;                // 整数矩阵网格（0 = 空，1 = 小行星）

    public int T;                  // 细胞自动机迭代次数
    public float asteroidSize;   // 每个小行星的大小（假设是立方体）

    public float playableAreaWidth;
    private int gridWidth;              // 网格宽度（列数）
    private int gridHeight;             // 网格高度（行数）

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
        // 获取游戏区域大小，基于代表它的立方体
        float gameAreaWidth = gameAreaObject.transform.localScale.x;
        float gameAreaHeight = gameAreaObject.transform.localScale.z;

        // 计算网格宽度和高度
        gridWidth = Mathf.FloorToInt(gameAreaWidth / asteroidSize);
        gridHeight = Mathf.FloorToInt(gameAreaHeight / asteroidSize);

        // 初始化网格
        m_Grid = new int[gridWidth, gridHeight];
    }

    void InitializeGrid()
    {
        // 随机初始化网格单元为0或1
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                m_Grid[x, y] = Random.Range(0, 2);  // 随机为0或1
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

                    // 应用规则
                    if (m_Grid[x, y] == 1)
                    {
                        newGrid[x, y] = (neighbors < 2 || neighbors > 3) ? 0 : 1; // 细胞死亡
                    }
                    else
                    {
                        newGrid[x, y] = (neighbors == 3) ? 1 : 0; // 细胞出生
                    }
                }
            }

            // 更新网格为新状态
            m_Grid = newGrid;
            DebugGrid();
        }
    }

    int CountAliveNeighbors(int x, int y)
    {
        int count = 0;

        // 遍历8个可能的邻居
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0) continue; // 跳过自身单元

                int nx = x + dx;
                int ny = y + dy;

                // 检查边界
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
            // 获取游戏区域的宽度
            float halfWidth = playableAreaWidth / 2;

            // 遍历网格的每个单元格
            for (int x = 0; x < m_Grid.GetLength(0); x++)
            {
                for (int y = 0; y < m_Grid.GetLength(1); y++)
                {
                    // 如果网格值为 1，则生成小行星
                    if (m_Grid[x, y] == 1)
                    {
                        // 计算小行星的 X 位置
                        // 将网格索引转换为游戏区域的 X 轴位置
                        float spawnX = (x - (m_Grid.GetLength(0) / 2)) * (playableAreaWidth / m_Grid.GetLength(0));
                        // 可以选择一个固定的 Z 位置，或者根据需要进行调整
                        float spawnZ = 0; // 根据需要设置 Z 轴位置

                        // 创建生成位置
                        Vector3 spawnPosition = new Vector3(spawnX, 0, spawnZ);
                        // 实例化小行星
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
