using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    void Awake()
    {
        animator.enabled = false;
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        animator.enabled = true;
        if (animator != null)
        {
        }

        yield return new WaitForSeconds(1f);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        animator.SetTrigger("Finish");

        while (!asyncOperation.isDone)
        {
            yield return null;
        }

    }
}