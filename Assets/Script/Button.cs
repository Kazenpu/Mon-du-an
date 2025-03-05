using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    internal object onClick;

    public object OnClick { get; internal set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level 0");
    }
    public void ExitGame()
    {
        Debug.Log("Đã thoát game");
        Application.Quit();
    }
}
