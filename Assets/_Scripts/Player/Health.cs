using System.Collections;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int m_maxHealth;
    [SerializeField] private int m_curHealth;
    [SerializeField] private float m_invulnerableDuration;
    [SerializeField] private Animator m_animator;
    [SerializeField] private IntEvent m_healthEvent;
    [SerializeField] private BoolEvent m_levelWonEvent;
    [SerializeField] private ActionEvent m_cameraShakeEvent;
    [SerializeField] private bool m_NoDamage;

    private bool m_isInvulnerable;
    private void Start()
    {
        m_curHealth = m_maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (m_NoDamage && Application.isEditor)
        {
            return;
        }
        
        if (m_isInvulnerable) return;
        
        m_curHealth -= damage;
        m_healthEvent.Raise(m_curHealth);
        m_cameraShakeEvent.Raise();
        
        if (m_curHealth <= 0)
        {
            StartCoroutine(ProcessKill());
            return;
        }
        
        StartCoroutine(OnInvulnerable());
    }

    private IEnumerator ProcessKill()
    {
        m_isInvulnerable = true;
        
        //Waiting for camera shake done <= dirty fix
        yield return new WaitForSecondsRealtime(0.5f); 
        //Raise event that the level is lost = player's dead
        m_levelWonEvent.Raise(false);
        Debug.Log("<color=red>Player DEAD</color>");
    }

    private IEnumerator OnInvulnerable()
    {
        m_isInvulnerable = true;
        m_animator.SetBool("IsInvulnerable", true);
        yield return new WaitForSeconds(m_invulnerableDuration);
        m_animator.SetBool("IsInvulnerable", false);
        m_isInvulnerable = false;
    }
}
