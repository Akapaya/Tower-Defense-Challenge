using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private int _sceneToUnload = 0;
    [SerializeField] private int _sceneToLoad = 0;
    [SerializeField] private GameObject _loadPanel;

    public void LoadScene()
    {
        _loadPanel.SetActive(true);
        StartCoroutine(getSceneLoadProgress());
    }

    IEnumerator getSceneLoadProgress()
    {
        yield return null;

        Time.timeScale = 1;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_sceneToLoad);
        asyncOperation.allowSceneActivation = false;
        float elapsedTime = 0f;
        float minimumLoadTime = 5f;

        while (!asyncOperation.isDone)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);

            if (asyncOperation.progress >= 0.9f && elapsedTime >= minimumLoadTime)
            {
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }

        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(_sceneToUnload);
        while (!unloadOperation.isDone)
        {
            yield return null;
        }
    }
}