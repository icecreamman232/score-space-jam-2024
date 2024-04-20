using JustGame.Script.Manager;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private BoolEvent m_winLevelEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerManager.PlayerLayer) return;
        
        Debug.Log("WIN");
        m_winLevelEvent.Raise(true);
    }
}
