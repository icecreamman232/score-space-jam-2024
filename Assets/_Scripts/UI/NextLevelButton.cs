using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NextLevelButton : Button
{
    [SerializeField] private AudioSource m_clickSound;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        m_clickSound.Play();
        base.OnPointerEnter(eventData);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.LoadLevel();
        base.OnPointerClick(eventData);
    }
}
