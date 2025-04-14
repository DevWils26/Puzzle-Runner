using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public void OnRestartButton(){

        SceneManager.LoadScene("Start Screen");

    }
}
