using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4]; // 0 - Up, 1 - Down, 2 - Right, 3 - Left
    }

    [System.Serializable]
    public class Rule
    {
        public GameObject room;
        public Vector2Int minPosition;
        public Vector2Int maxPosition;
        public bool obligatory;

        public int ProbabilityOfSpawning(int x, int y)
        {
            if (x >= minPosition.x && x <= maxPosition.x && y >= minPosition.y && y <= maxPosition.y)
            {
                return obligatory ? 2 : 1;
            }
            return 0;
        }
    }

    public Vector2Int size;
    public int startPos = 0;
    public Rule[] rooms;
    public Vector2 offset;

    public GameObject startMarker;
    public GameObject endMarker;
    public GameObject playerPrefab;

    List<Cell> board;
    int finishCell;

    void Start()
    {
        MazeGenerator();
    }

    void GenerateDungeon()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Cell currentCell = board[i + j * size.x];
                if (currentCell.visited)
                {
                    int randomRoom = -1;
                    List<int> availableRooms = new List<int>();

                    for (int k = 0; k < rooms.Length; k++)
                    {
                        int p = rooms[k].ProbabilityOfSpawning(i, j);
                        if (p == 2)
                        {
                            randomRoom = k;
                            break;
                        }
                        else if (p == 1)
                        {
                            availableRooms.Add(k);
                        }
                    }

                    if (randomRoom == -1)
                    {
                        randomRoom = availableRooms.Count > 0 ? availableRooms[Random.Range(0, availableRooms.Count)] : 0;
                    }

                    var roomObj = Instantiate(rooms[randomRoom].room, new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity, transform);
                    var newRoom = roomObj.GetComponent<RoomBehaviour>();
                    newRoom.UpdateRoom(currentCell.status);
                    roomObj.name += $" {i}-{j}";

                    // Start room marker
                    if (i + j * size.x == startPos && startMarker != null)
                    {
                        Instantiate(startMarker, roomObj.transform.position + Vector3.up * 1.5f, Quaternion.identity);
                        Instantiate(playerPrefab, roomObj.transform.position + Vector3.up * 2f, Quaternion.identity);
                    }

                    // Finish room marker
                    if (i + j * size.x == finishCell && endMarker != null)
                    {
                        Instantiate(endMarker, roomObj.transform.position + Vector3.up * 1.5f, Quaternion.identity);
                    }
                }
            }
        }
    }

    void MazeGenerator()
    {
        board = new List<Cell>();
        for (int i = 0; i < size.x * size.y; i++)
            board.Add(new Cell());

        int currentCell = startPos;
        Stack<int> path = new Stack<int>();
        int k = 0;

        while (k < 1000)
        {
            k++;
            board[currentCell].visited = true;
            List<int> neighbors = CheckNeighbors(currentCell);

            if (neighbors.Count == 0)
            {
                if (path.Count == 0) break;
                currentCell = path.Pop();
            }
            else
            {
                path.Push(currentCell);
                int newCell = neighbors[Random.Range(0, neighbors.Count)];

                // Connecting walls
                int diff = newCell - currentCell;

                if (diff == 1)
                {
                    board[currentCell].status[2] = true;
                    board[newCell].status[3] = true;
                }
                else if (diff == -1)
                {
                    board[currentCell].status[3] = true;
                    board[newCell].status[2] = true;
                }
                else if (diff == size.x)
                {
                    board[currentCell].status[1] = true;
                    board[newCell].status[0] = true;
                }
                else if (diff == -size.x)
                {
                    board[currentCell].status[0] = true;
                    board[newCell].status[1] = true;
                }

                currentCell = newCell;
                finishCell = newCell; // Keep updating finish cell to the deepest
            }
        }

        GenerateDungeon();
    }

    List<int> CheckNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();

        if (cell - size.x >= 0 && !board[cell - size.x].visited)
            neighbors.Add(cell - size.x);
        if (cell + size.x < board.Count && !board[cell + size.x].visited)
            neighbors.Add(cell + size.x);
        if ((cell + 1) % size.x != 0 && !board[cell + 1].visited)
            neighbors.Add(cell + 1);
        if (cell % size.x != 0 && !board[cell - 1].visited)
            neighbors.Add(cell - 1);

        return neighbors;
    }
}
