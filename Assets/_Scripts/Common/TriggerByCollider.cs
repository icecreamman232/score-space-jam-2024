using JustGame.Script.Manager;
using UnityEngine;
using UnityEngine.Events;

public class TriggerByCollider : MonoBehaviour
{
   [SerializeField] private LayerMask m_targetMask;
   [SerializeField] private UnityEvent m_eventToTrigger;
   [SerializeField] private bool m_onlyOnce;
   private bool m_isTriggered;
   
   private void OnTriggerEnter2D(Collider2D other)
   {
      if (m_onlyOnce && m_isTriggered)
      {
         return;
      }
      
      if (LayerManager.IsInLayerMask(other.gameObject.layer, m_targetMask))
      {
         m_eventToTrigger?.Invoke();
         m_isTriggered = true;
      }
   }
}
