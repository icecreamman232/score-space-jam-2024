using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] private CanvasGroup m_canvasGroup;
    [SerializeField] private BoolEvent m_levelWonEvent;
    
    private void Start()
    {
        Show();
        m_levelWonEvent.AddListener(OnLevelWon);
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
