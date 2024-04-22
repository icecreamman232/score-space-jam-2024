using System.Collections;
using DG.Tweening;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

public class EndingScreenController : MonoBehaviour
{
    [SerializeField] private CanvasGroup m_canvasGroup_1;
    [SerializeField] private CanvasGroup m_canvasGroup_2;
    [SerializeField] private CanvasGroup m_canvasGroup_3;
    [SerializeField] private CanvasGroup m_canvasGroup_4;
    [SerializeField] private ActionEvent m_endGameEvent;

    private bool m_isDone;

    private void Start()
    {
        m_endGameEvent.AddListener(OnEndGame);
    }

    private void OnDestroy()
    {
        m_endGameEvent.RemoveListener(OnEndGame);
    }

    private void OnEndGame()
    {
        PlayEnding();
    }

    private void PlayEnding()
    {
        StartCoroutine(EndingRoutine());
    }

    private void Update()
    {
        if (Input.anyKeyDown && m_isDone)
        {
            SceneLoader.Instance.LoadScene("GameplayScene","MenuScene");
        }
    }

    private IEnumerator EndingRoutine()
    {
        m_isDone = false;
        m_canvasGroup_1.DOFade(1, 0.5f).SetUpdate(true);
        yield return new WaitForSecondsRealtime(0.5f);
        yield return new WaitForSecondsRealtime(1.5f);
        m_canvasGroup_2.DOFade(1, 0.5f).SetUpdate(true);
        yield return new WaitForSecondsRealtime(0.5f);
        yield return new WaitForSecondsRealtime(1.5f);
        m_canvasGroup_3.DOFade(1, 0.5f).SetUpdate(true);
        yield return new WaitForSecondsRealtime(0.5f);
        m_canvasGroup_4.DOFade(1, 0.3f).SetUpdate(true);
        yield return new WaitForSecondsRealtime(1f);
        m_isDone = true;
    }
}
