using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] private CanvasGroup m_canvasGroup;
    [SerializeField] private BoolEvent m_levelWonEvent;
    [SerializeField] private ActionEvent m_loadLevelDoneEvent;
    
    private void Start()
    {
        Hide();
        m_levelWonEvent.AddListener(OnLevelWon);
        m_loadLevelDoneEvent.AddListener(OnLoadLevelDone);
    }

    private void OnDestroy()
    {
        m_levelWonEvent.RemoveListener(OnLevelWon);
        m_loadLevelDoneEvent.RemoveListener(OnLoadLevelDone);
    }

    private void OnLoadLevelDone()
    {
        Show();
    }

    private void OnLevelWon(bool isWin)
    {
        Hide();
    }

    private void Show()
    {
        m_canvasGroup.alpha = 1;
        m_canvasGroup.interactable = false;
        m_canvasGroup.blocksRaycasts = false;
    }

    private void Hide()
    {
        m_canvasGroup.alpha = 0;
        m_canvasGroup.interactable = false;
        m_canvasGroup.blocksRaycasts = false;
    }
}
