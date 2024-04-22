using System;
using DG.Tweening;
using JustGame.Scripts.ScriptableEvent;
using TMPro;
using UnityEngine;

public class OpeningLevelUIController : MonoBehaviour
{
    [SerializeField] private RectTransform m_textRT;
    [SerializeField] private TextMeshProUGUI m_floorTxt;
    [SerializeField] private Ease m_ease;
    [SerializeField] private ActionEvent m_callToPlayEvent;

    private void Awake()
    {
        m_callToPlayEvent.AddListener(PlayOpening);
    }
    
    private void PlayOpening()
    {
        m_floorTxt.text = $"Floor {GameManager.Instance.FloorNumber}";
        m_textRT.anchoredPosition = new Vector2(0, -117);
        m_textRT.DOLocalMoveY(0, 0.3f)
            .SetUpdate(true)
            .SetEase(m_ease)
            .OnComplete(() =>
        {
            m_textRT.DOLocalMoveY(117, 0.3f)
                .SetUpdate(true)
                .SetDelay(0.7f)
                .SetEase(m_ease);
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayOpening();
        }
    }
}
