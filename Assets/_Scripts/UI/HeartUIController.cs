using System;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

public class HeartUIController : MonoBehaviour
{
    [SerializeField] private GameObject[] m_heartArr;
    [SerializeField] private IntEvent m_healthEvent;

    private void Start()
    {
        m_healthEvent.AddListener(OnUpdateHealth);
    }

    private void OnDestroy()
    {
        m_healthEvent.RemoveListener(OnUpdateHealth);
    }

    private void OnUpdateHealth(int curHealth)
    {
        if (curHealth == 2)
        {
            m_heartArr[2].SetActive(false);
        }
        else if (curHealth == 1)
        {
            m_heartArr[1].SetActive(false);
        }
        else
        {
            m_heartArr[0].SetActive(false);
        }
    }
}
