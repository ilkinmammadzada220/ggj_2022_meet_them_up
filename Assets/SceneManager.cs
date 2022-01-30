using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadScene(int index)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }

    private void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene()
            .buildIndex);
    }

    public void ReloadSceneWithDelay()
    {
        StartCoroutine(CoReloadScene());
    }

    private IEnumerator CoReloadScene()
    {
        yield return new WaitForSeconds(3f);
        ReloadScene();
    }

    public void LoadNextScene()
    {
        if (UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings - 1 == UnityEngine.SceneManagement.SceneManager
            .GetActiveScene().buildIndex)
            LoadScene(0);
        else
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene()
                .buildIndex + 1);
    }

    public void LoadNextSceneWithDelay()
    {
        StartCoroutine(CoLoadNextScene());
    }

    private IEnumerator CoLoadNextScene()
    {
        yield return new WaitForSeconds(3f);
        LoadNextScene();
    }

    public void Quit()
    {
        Application.Quit(1);
    }
}