using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]int currentLevel = 0;
    [SerializeField]bool sceneLoading = false;

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        int numLevelManagers = FindObjectsOfType<LevelManager>().Length;
        if (numLevelManagers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sceneLoading = false;
    }

    public void NextLevel(float waitTime) {
        if (!sceneLoading)
        {
            currentLevel++;
            sceneLoading = true;
            StartCoroutine(EnuNextLevel(waitTime));
        }
    }

    public void RestartLevel(float waitTime) {
        if (!sceneLoading)
        {
            sceneLoading = true;
            StartCoroutine(Restart(waitTime));
        }
    }

    IEnumerator Restart(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(currentLevel);
    }

    IEnumerator EnuNextLevel(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(currentLevel);
    }
}
