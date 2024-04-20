using System;
using JustGame.Scripts.ScriptableEvent;
using TMPro;
using UnityEngine;

public class TimerUIController : MonoBehaviour
{
    [Header("Value")]
    [SerializeField] private int m_curMin;
    [SerializeField] private int m_curSec;
    [SerializeField] private BoolEvent m_levelWinEvent;
    [Header("UI")] 
    [SerializeField] private TextMeshProUGUI m_timerText;

    private float m_timer;

    private Action<int, int> OnUpdateTime;

    private void Start()
    {
        m_curMin = 0;
        m_curSec = 0;
        m_timerText.text = "00:00";
        OnUpdateTime += GameManager.Instance.RecordTime;
    }

    private void OnDestroy()
    {
        OnUpdateTime -= GameManager.Instance.RecordTime;
    }
    
    private void Update()
    {
        m_timer += Time.deltaTime;
        if (m_timer >= 1)
        {
            m_timer = 0;
            CountTime();
        }
    }

    private void CountTime()
    {
        m_curSec++;
        if (m_curSec >= 60)
        {
            m_curMin++;
            m_curSec = 0;
        }

        OnUpdateTime?.Invoke(m_curMin,m_curSec);
        m_timerText.text = $"{m_curMin:00}:{m_curSec:00}";
    }
}
