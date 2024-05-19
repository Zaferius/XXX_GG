using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject cellPrefab; 
    private GameObject[,] grid;
    private Camera cam;

    public int rows = 3; 
    public int columns = 3; 
    private float cellSize = 1.0f;

    private void Start()
    {
        cam = Camera.main;
        CreateGrid();
    }

    private void CreateGrid()
    {
        grid = new GameObject[rows, columns];

        var orthographicSize = cam.orthographicSize;
        var screenWidth = orthographicSize * 2.0f * Screen.width / Screen.height;
        var screenHeight = orthographicSize * 2.0f;
        var cellSize = Mathf.Min(screenWidth / columns, screenHeight / rows);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                var position = new Vector3(col * cellSize, row * -cellSize, 0);
                var cell = Instantiate(cellPrefab, position, Quaternion.identity);
                cell.transform.parent = transform;
                cell.transform.localScale = new Vector3(cellSize, cellSize, 1);
                grid[row, col] = cell;
            }
        }
        
        var gridWidth = columns * cellSize;
        var gridHeight = rows * cellSize;
        transform.position = new Vector3(-gridWidth / 2 + cellSize / 2, screenHeight / 2 - cellSize / 2, 0);
    }
}
