using UnityEngine;
using UnityEngine.Events;

public class SwitchConditionController : MonoBehaviour
{

    [SerializeField] private CircleAsSwitch[] m_switchList;
    [SerializeField] private bool[] m_order;
    [SerializeField] private UnityEvent m_trigger;
    
    public void CheckCondition()
    {
        var check = true;
        for (int i = 0; i < m_switchList.Length; i++)
        {
            if (m_switchList[i].IsOn != m_order[i])
            {
                check = false;
                break;
            }
        }

        if (check)
        {
            m_trigger?.Invoke();
        }
    }
}
