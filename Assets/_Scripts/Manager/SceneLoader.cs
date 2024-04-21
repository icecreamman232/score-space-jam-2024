using System.Collections;
using JustGame.Script.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : PersistentSingleton<SceneLoader>
{
    private bool m_isLoading;
    public bool IsLoadDone => m_isLoading;
    
    public void LoadScene(string from, string to)
    {
        StartCoroutine(LoadSceneRoutine(from, to));
    }

    private IEnumerator LoadSceneRoutine(string from, string to)
    {
        if (m_isLoading)
        {
            yield break;
        }
        m_isLoading = true;
        
        var loadLoadingScene = SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Additive);
        yield return new WaitUntil(() => loadLoadingScene.isDone);
        
        var unload = SceneManager.UnloadSceneAsync(from);
        yield return new WaitUntil(() => unload.isDone);

        yield return new WaitForSecondsRealtime(1);
        
        var load = SceneManager.LoadSceneAsync(to,LoadSceneMode.Additive);
        yield return new WaitUntil(() => load.isDone);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(to));
        
        LoadingSceneController.Instance.FadeOut();
        yield return new WaitForSecondsRealtime(LoadingSceneController.Instance.Duration);
        
        var unloadLoadingScene = SceneManager.UnloadSceneAsync("LoadingScene");
        yield return new WaitUntil(() => unloadLoadingScene.isDone);

        m_isLoading = false;
    }
}
