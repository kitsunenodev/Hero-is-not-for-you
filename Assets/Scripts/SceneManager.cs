using UnityEngine;
using _sceneManger = UnityEngine.SceneManagement;
public class SceneManager : MonoBehaviour
{
    public string ControlsSceneName;

    public string InfiniteSceneName;

    public string MainGameSceneName;

    public string MainMenuSceneName;

    public string GameOverSceneName;

    public string VictorySceneName;

    void LoadScene(string scene)
    {
        _sceneManger.SceneManager.LoadScene(scene);
    }

    public void LoadControlScene()
    {
        LoadScene(ControlsSceneName);
    }
    
    public void LoadMainGameScene()
    {
        LoadScene(MainGameSceneName);
    }
    
    public void LoadInfiniteScene()
    {
        LoadScene(InfiniteSceneName);
    }
    
    public void LoadMainMenuScene()
    {
        LoadScene(MainMenuSceneName);
    }

    public void LoadGameOverScene()
    {
        LoadScene(GameOverSceneName);
    }

    public void LoadVictoryScene()
    {
        LoadScene(VictorySceneName);
    }
}
