using System;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager i;
    
    public GameObject cellPrefab; 
    private GameObject[,] _grid;
    private Camera _cam;

    [HideInInspector] public int size = 5;

    public int matchCount;

    private void Awake()
    {
        i = this;
    }

    private void Start()
    {
        _cam = Camera.main;
        CreateGrid();
    }

    public void CreateGrid()
    {
        matchCount = 0;
        Actions.OnMatchDataChanged();
        
        transform.localPosition = Vector3.zero;
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        _grid = new GameObject[size, size];

        var orthographicSize = _cam.orthographicSize;
        var screenWidth = orthographicSize * 2.0f * Screen.width / Screen.height;
        var screenHeight = orthographicSize * 2.0f;
        var cellSize = Mathf.Min(screenWidth / size, screenHeight / size);

        for (var row = 0; row < size; row++)
        {
            for (var col = 0; col < size; col++)
            {
                var position = new Vector3(col * cellSize, row * -cellSize, 0);
                var cell = Instantiate(cellPrefab, position, Quaternion.identity);
                var cellSc = cell.GetComponent<Cell>();
                cell.transform.parent = transform;
                cell.transform.localScale = new Vector3(cellSize, cellSize, 1);
                _grid[row, col] = cell;
                cellSc.row = row;
                cellSc.column = col;
            }
        }
        
        var gridWidth = size * cellSize;
        transform.position = new Vector3(-gridWidth / 2 + cellSize / 2, screenHeight / 2 - cellSize / 2, 0);
    }
    
    
    public void CheckMatches()
    {
        bool[,] visited = new bool[size, size];

        for (var row = 0; row < size; row++)
        {
            for (var col = 0; col < size; col++)
            {
                if (_grid[row, col].GetComponent<Cell>().isTaken && !visited[row, col])
                {
                    List<Cell> matchedCells = new List<Cell>();
                    FloodFill(row, col, visited, matchedCells);

                    if (matchedCells.Count >= 3)
                    {
                        foreach (var cell in matchedCells)
                        {
                            cell.ClearCell();
                        }
                        
                        matchCount++;
                        Actions.OnMatchDataChanged();
                    }
                }
            }
        }
    }

    private void FloodFill(int row, int col, bool[,] visited, List<Cell> matchedCells)
    {
        Queue<Cell> queue = new Queue<Cell>();
        queue.Enqueue(_grid[row, col].GetComponent<Cell>());

        while (queue.Count > 0)
        {
            Cell current = queue.Dequeue();
            int curRow = current.row;
            int curCol = current.column;

            if (visited[curRow, curCol])
                continue;

            visited[curRow, curCol] = true;
            matchedCells.Add(current);

            foreach (var neighbor in GetNeighbors(curRow, curCol))
            {
                if (!visited[neighbor.row, neighbor.column] && neighbor.isTaken)
                {
                    queue.Enqueue(neighbor);
                }
            }
        }
    }

    private List<Cell> GetNeighbors(int row, int col)
    {
        var neighbors = new List<Cell>();

        if (row > 0) neighbors.Add(_grid[row - 1, col].GetComponent<Cell>());
        if (row < size - 1) neighbors.Add(_grid[row + 1, col].GetComponent<Cell>());
        if (col > 0) neighbors.Add(_grid[row, col - 1].GetComponent<Cell>());
        if (col < size - 1) neighbors.Add(_grid[row, col + 1].GetComponent<Cell>());

        return neighbors;
    }
}
