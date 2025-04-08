using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField]
    private MazeCellObject _mazeCellPrefab;

    [SerializeField]
    public int _mazeWidth;

    [SerializeField]
    public int _mazeDepth;

    private MazeCellObject[,] _mazeGrid;
    private MazeCellObject _startCell;
    private MazeCellObject _finishCell;

    void Start()
    {
        _mazeGrid = new MazeCellObject[_mazeWidth, _mazeDepth];

        // Instantiate the maze grid
        for (int x = 0; x < _mazeWidth; x++) 
        {
            for (int z = 0; z < _mazeDepth; z++)
            {
                _mazeGrid[x, z] = Instantiate(_mazeCellPrefab, new Vector3(x, 0, z), Quaternion.identity);
            }
        }

        // Generate the maze
        GenerateMaze(null, _mazeGrid[0, 0]);

        // After maze generation, set start and finish
        SetStartAndFinish();
    }

    private void SetStartAndFinish()
    {
        // Randomize start and finish positions
        int startX = Random.Range(0, _mazeWidth);
        int startZ = Random.Range(0, _mazeDepth);

        int finishX = Random.Range(0, _mazeWidth);
        int finishZ = Random.Range(0, _mazeDepth);

        // Ensure start and finish are not the same
        while (startX == finishX && startZ == finishZ)
        {
            finishX = Random.Range(0, _mazeWidth);
            finishZ = Random.Range(0, _mazeDepth);
        }

        // Set the start cell and finish cell
        _startCell = _mazeGrid[startX, startZ];
        _startCell.SetAsStart();  // Set start cell color to green

        _finishCell = _mazeGrid[finishX, finishZ];
        _finishCell.SetAsFinish();  // Set finish cell color to red
    }

    private void GenerateMaze(MazeCellObject previousCell, MazeCellObject currentCell)
    {
        currentCell.Visit();
        ClearWalls(previousCell, currentCell);

        MazeCellObject nextCell;

        do 
        {
            nextCell = GetNextUnvisitedCell(currentCell);

            if (nextCell != null) 
            {
                GenerateMaze(currentCell, nextCell);
            }
        } while (nextCell != null);
    }

    private MazeCellObject GetNextUnvisitedCell(MazeCellObject currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);
        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
    }

    private IEnumerable<MazeCellObject> GetUnvisitedCells(MazeCellObject currentCell)
    {
        int x = (int)currentCell.transform.position.x;
        int z = (int)currentCell.transform.position.z;

        if (x + 1 < _mazeWidth)
        {
            var cellToRight = _mazeGrid[x + 1, z];
            if (!cellToRight.IsVisited)
            {
                yield return cellToRight;
            }
        }

        if (x - 1 >= 0)
        {
            var cellToLeft = _mazeGrid[x - 1, z];
            if (!cellToLeft.IsVisited)
            {
                yield return cellToLeft;
            }
        }

        if (z + 1 < _mazeDepth)
        {
            var cellToFront = _mazeGrid[x, z + 1];
            if (!cellToFront.IsVisited)
            {
                yield return cellToFront;
            }
        }

        if (z - 1 >= 0)
        {
            var cellToBack = _mazeGrid[x, z - 1];
            if (!cellToBack.IsVisited)
            {
                yield return cellToBack;
            }
        }
    }

    private void ClearWalls(MazeCellObject previousCell, MazeCellObject currentCell)
    {
        if (previousCell == null)
        {
            return;
        }

        if (previousCell.transform.position.x < currentCell.transform.position.x)
        {
            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
            return;
        }

        if (previousCell.transform.position.x > currentCell.transform.position.x)
        {
            previousCell.ClearLeftWall();
            currentCell.ClearRightWall();
            return;
        }

        if (previousCell.transform.position.z < currentCell.transform.position.z)
        {
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
            return;
        }

        if (previousCell.transform.position.z > currentCell.transform.position.z)
        {
            previousCell.ClearBackWall();
            currentCell.ClearFrontWall();
            return;
        }
    }
}
