using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    public GameObject[] walls; // 0 - Up 1 -Down 2 - Right 3- Left
    public GameObject[] doors;

    public void UpdateRoom(bool[] status)
    {
        for (int i = 0; i < status.Length; i++)
        {
            if (i < doors.Length && doors[i] != null)
                doors[i].SetActive(status[i]);

            if (i < walls.Length && walls[i] != null)
                walls[i].SetActive(!status[i]);
        }
    }
}