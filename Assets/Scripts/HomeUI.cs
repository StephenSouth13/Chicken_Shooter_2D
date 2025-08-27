using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeUI : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Scene_1"); // tên scene bạn muốn chuyển tới
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}