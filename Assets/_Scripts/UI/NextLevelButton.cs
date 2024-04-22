using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NextLevelButton : Button
{
    [SerializeField] private GameObject m_bg;
    [SerializeField] private GameObject m_underline;
    [SerializeField] private AudioSource m_clickSound;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        m_bg.SetActive(true);
        m_underline.SetActive(true);
        m_clickSound.Play();
        base.OnPointerEnter(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        m_bg.SetActive(false);
        m_underline.SetActive(false);
        base.OnPointerExit(eventData);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.Replay();
        base.OnPointerClick(eventData);
    }
}
