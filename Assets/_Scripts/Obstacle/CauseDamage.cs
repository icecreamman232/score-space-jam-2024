using UnityEngine;

public class CauseDamage : MonoBehaviour
{
    [SerializeField] private int m_damageCause;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        var health = other.GetComponent<Health>();
        if (health == null) return;
        health.TakeDamage(m_damageCause);
    }
}
