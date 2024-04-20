using System;
using TMPro;
using UnityEngine;

public class TimerUIController : MonoBehaviour
{
    [Header("Value")]
    [SerializeField] private int m_curMin;
    [SerializeField] private int m_curSec;
    [Header("UI")] 
    [SerializeField] private TextMeshProUGUI m_timerText;

    private float m_timer;

    private void Start()
    {
        m_curMin = 0;
        m_curSec = 0;
        m_timerText.text = "00:00";
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

        m_timerText.text = $"{m_curMin:00}:{m_curSec:00}";
    }
}
