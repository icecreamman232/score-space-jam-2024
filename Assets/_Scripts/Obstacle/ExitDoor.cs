using System;
using System.Collections;
using JustGame.Script.Manager;

using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private BoolEvent m_winLevelEvent;
    [SerializeField] private Animator m_animator;
    private bool m_hasProcess;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerManager.PlayerLayer) return;
        
        StartCoroutine(PlayRevealAnim(other.gameObject));
    }

    private IEnumerator PlayRevealAnim(GameObject player)
    {
        if (m_hasProcess)
        {
            yield break;
        }

        m_hasProcess = true;

        m_spriteRenderer.maskInteraction = SpriteMaskInteraction.None;
        
        var movementPlayer = player.GetComponent<PlayerMovement>();
        if (movementPlayer != null)
        {
            movementPlayer.MoveToExitDoor(this.transform);
        }

        yield return new WaitForSecondsRealtime(1f);
        
        m_hasProcess = false;
        m_winLevelEvent.Raise(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position,0.3f);
    }
}
