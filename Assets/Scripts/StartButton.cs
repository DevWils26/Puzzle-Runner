using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void OnStartButton(){

        SceneManager.LoadScene("Level_1 Maze");

    }

}
