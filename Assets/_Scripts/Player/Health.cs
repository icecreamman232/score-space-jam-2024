using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int m_maxHealth;
    [SerializeField] private int m_curHealth;
    [SerializeField] private float m_invulnerableDuration;

    private bool m_isInvulnerable;
    private void Start()
    {
        m_curHealth = m_maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (m_isInvulnerable) return;
        
        m_curHealth -= damage;

        if (m_curHealth <= 0)
        {
            ProcessKill();
            return;
        }
        
        StartCoroutine(OnInvulnerable());
    }

    private void ProcessKill()
    {
        m_isInvulnerable = true;
        Destroy(this.gameObject);
        Debug.Log($"<color=red>Player DEAD<color/>");
    }

    private IEnumerator OnInvulnerable()
    {
        m_isInvulnerable = true;
        yield return new WaitForSeconds(m_invulnerableDuration);
        m_isInvulnerable = false;
    }
}
