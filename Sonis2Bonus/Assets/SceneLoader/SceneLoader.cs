using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] GameObject loadingScreen;

    [SerializeField] Animator transition;

    [SerializeField] float progress;

    public static SceneLoader instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        LoadScene("Game");
    }

    public void LoadScene(string scene)
    {
        StartCoroutine(LoadAsynchronously(scene));
    }

    IEnumerator LoadAsynchronously(string scene)
    {
        Time.timeScale = 0;
        loadingScreen.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        while (!operation.isDone)
        {
            progress = Mathf.Clamp01(operation.progress / .9f);
            progress *= 100f ;
            yield return null;
        }

        Time.timeScale = 1;
        loadingScreen.SetActive(false);
    }
}

/*
 
        Time.timeScale = 0;
        transition.SetTrigger("End");

        yield return new WaitForSecondsRealtime(transition.GetCurrentAnimatorStateInfo(0).normalizedTime % 1);

        loadingScreen.SetActive(true);

        txtWorld.text = "World " + GameManager.instance.level.ToString();

        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            yield return null;
        }

        Time.timeScale = 1;
        transition.SetTrigger("Start");
        loadingScreen.SetActive(false);
 */