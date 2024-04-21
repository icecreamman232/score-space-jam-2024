using System;
using DG.Tweening;
using JustGame.Script.Manager;
using UnityEngine;

public class LoadingSceneController : Singleton<LoadingSceneController>
{
    [SerializeField] private CanvasGroup m_canvasGroup;
    [SerializeField] private float m_fadeDuration;

    private bool m_isLoadDone;

    public float Duration => m_fadeDuration;
    public Action OnLoadDone;
    public bool IsLoadDone => m_isLoadDone;

    private bool m_isLoadingScene;
    
    private void Start()
    {
        FadeIn();
    }

    private void OnLoadComplete()
    {
        m_isLoadDone = true;
        OnLoadDone?.Invoke();
    }
    
    public void FadeIn()
    {
        m_isLoadDone = false;
        m_canvasGroup.DOFade(1.0f, m_fadeDuration).SetUpdate(true).OnComplete(OnLoadComplete);
    }

    public void FadeOut()
    {
        m_isLoadDone = false;
        m_canvasGroup.DOFade(0, m_fadeDuration).SetUpdate(true).OnComplete(OnLoadComplete);
    }
}
