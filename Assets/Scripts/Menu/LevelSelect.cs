using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void PlayGame(string levelName)
    {
        Collectable.ResetCollectables();
        PlayerRespawn.ResetDeaths();
        SceneManager.LoadSceneAsync(levelName);
    }

    public void GoBack()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
