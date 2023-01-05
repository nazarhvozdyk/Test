using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameManager();

            return _instance;
        }
    }
    private static GameManager _instance;

    public void OnPlayerDead()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
