using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;
 
    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
        }else{
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void Win()
    {
        SceneManager.Instance.LoadNextSceneWithDelay();
    }

    public void Lose()
    {
        SceneManager.Instance.ReloadSceneWithDelay();
    }
}
