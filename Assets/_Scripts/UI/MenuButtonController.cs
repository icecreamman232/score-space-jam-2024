using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButtonController : Button
{
    [SerializeField] private GameObject m_underline;
    [SerializeField] private GameObject m_highlightBG;
    [SerializeField] private AudioSource m_clickSound;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        m_underline.SetActive(true);
        m_highlightBG.SetActive(true);
        m_clickSound.Play();
        base.OnPointerEnter(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        m_underline.SetActive(false);
        m_highlightBG.SetActive(false);
        base.OnPointerExit(eventData);
    }
}
