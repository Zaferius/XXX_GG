using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject cellPrefab; 
    private GameObject[,] grid;

    public int rows = 3; 
    public int columns = 3; 
    private float cellSize = 1.0f;

    private void Start()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        grid = new GameObject[rows, columns];

        for (var row = 0; row < rows; row++)
        {
            for (var col = 0; col < columns; col++)
            {
                var cell = Instantiate(cellPrefab, new Vector3(col * cellSize, row * -cellSize, 0), Quaternion.identity);
                cell.transform.parent = transform;
                grid[row, col] = cell;
            }
        }
    }
}
