using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelSelect : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    public void PlayGame(int level)
    {
        SceneManager.LoadSceneAsync(level);
    }

    public void GoBack()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
