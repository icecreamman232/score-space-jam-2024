using JustGame.Scripts.ScriptableEvent;
using TMPro;
using UnityEngine;

public class LevelWinUIController : MonoBehaviour
{
    [SerializeField] private CanvasGroup m_canvasGroup;
    [SerializeField] private TextMeshProUGUI m_timeTxt;
    [SerializeField] private BoolEvent m_levelWonEvent;

    private void Start()
    {
        Hide();
        m_levelWonEvent.AddListener(OnLevelWon);
    }

    private void OnDestroy()
    {
        m_levelWonEvent.RemoveListener(OnLevelWon);
    }

    private void OnLevelWon(bool isWon)
    {
        if (!isWon) return;
        Show();
        var gameManager = GameManager.Instance;
        m_timeTxt.text = $"{gameManager.LastMinute:00}:{gameManager.LastSeconds:00}";
    }

    private void Show()
    {
        m_canvasGroup.alpha = 1;
        m_canvasGroup.interactable = true;
        m_canvasGroup.blocksRaycasts = true;
    }

    public void Hide()
    {
        m_canvasGroup.alpha = 0;
        m_canvasGroup.interactable = false;
        m_canvasGroup.blocksRaycasts = false;
    }
}