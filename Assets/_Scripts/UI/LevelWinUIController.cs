using DG.Tweening;
using JustGame.Scripts.ScriptableEvent;
using TMPro;
using UnityEngine;

public class LevelWinUIController : MonoBehaviour
{
    [SerializeField] private CanvasGroup m_canvasGroup;
    [SerializeField] private TextMeshProUGUI m_timeTxt;
    [SerializeField] private TextMeshProUGUI m_floorTxt;
    [SerializeField] private DOTweenAnimation m_floorTween;
    [SerializeField] private BoolEvent m_levelWonEvent;

    private void Start()
    {
        Hide();
        m_levelWonEvent.AddListener(OnLevelWon);
        m_timeTxt.transform.parent.gameObject.SetActive(false);
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
        m_floorTxt.text = $"Floor {GameManager.Instance.FloorNumber}";
        m_floorTween.DORestart();
        m_timeTxt.text = $"{gameManager.LastMinute:00}:{gameManager.LastSeconds:00}";
    }

    private void Show()
    {
        m_canvasGroup.alpha = 1;
        m_canvasGroup.interactable = true;
        m_canvasGroup.blocksRaycasts = true;
        m_timeTxt.transform.parent.gameObject.SetActive(false);
    }

    public void Hide()
    {
        m_canvasGroup.alpha = 0;
        m_canvasGroup.interactable = false;
        m_canvasGroup.blocksRaycasts = false;
    }
}
