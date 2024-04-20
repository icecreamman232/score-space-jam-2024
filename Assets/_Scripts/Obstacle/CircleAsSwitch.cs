using JustGame.Script.Manager;
using UnityEngine;
using UnityEngine.Events;

public class CircleAsSwitch : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private Color m_onColor;
    [SerializeField] private Color m_offColor;
    [SerializeField] private bool m_isOn;

    [SerializeField] private UnityEvent m_triggerWhenOn;
    [SerializeField] private UnityEvent m_triggerWhenOff;

    private void Start()
    {
        m_spriteRenderer.color = m_isOn ? m_onColor : m_offColor;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerManager.PlayerLayer)
        {
            return;
        }

        TriggerSwitch();
    }

    private void TriggerSwitch()
    {
        m_isOn = !m_isOn;
        m_spriteRenderer.color = m_isOn ? m_onColor : m_offColor;
        if (m_isOn)
        {
            m_triggerWhenOn?.Invoke();
        }
        else
        {
            m_triggerWhenOff?.Invoke();
        }
    }
}
