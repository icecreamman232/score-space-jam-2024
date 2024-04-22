using System;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

public class HeartUIController : MonoBehaviour
{
    [SerializeField] private GameObject[] m_heartArr;
    [SerializeField] private IntEvent m_healthEvent;
    [SerializeField] private ActionEvent m_loadLevelDone;

    private void Start()
    {
        m_healthEvent.AddListener(OnUpdateHealth);
        m_loadLevelDone.AddListener(OnLoadLevelDone);
    }
    private void OnDestroy()
    {
        m_healthEvent.RemoveListener(OnUpdateHealth);
        m_loadLevelDone.RemoveListener(OnLoadLevelDone);
    }

    private void OnLoadLevelDone()
    {
        // foreach (var heart in m_heartArr)
        // {
        //     heart.SetActive(true);
        // }
    }
    
    private void OnUpdateHealth(int curHealth)
    {
        if (curHealth == 3)
        {
            m_heartArr[0].SetActive(true);
            m_heartArr[1].SetActive(true);
            m_heartArr[2].SetActive(true);
        }
        else if (curHealth == 2)
        {
            m_heartArr[0].SetActive(true);
            m_heartArr[1].SetActive(true);
            m_heartArr[2].SetActive(false);
        }
        else if (curHealth == 1)
        {
            m_heartArr[0].SetActive(true);
            m_heartArr[1].SetActive(false);
            m_heartArr[2].SetActive(false);
        }
        else
        {
            m_heartArr[0].SetActive(false);
            m_heartArr[1].SetActive(false);
            m_heartArr[2].SetActive(false);
        }
    }
}
