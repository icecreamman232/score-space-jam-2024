using DG.Tweening;
using JustGame.Scripts.ScriptableEvent;
using TMPro;
using UnityEngine;

public class LevelLostUIController : MonoBehaviour
{
    [SerializeField] private CanvasGroup m_canvasGroup;
    [SerializeField] private TextMeshProUGUI m_floorTxt;
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
        if (isWon) return;
        Show();
        var gameManager = GameManager.Instance;
        m_floorTxt.text = $"Highest floor {GameManager.Instance.FloorNumber}";
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
