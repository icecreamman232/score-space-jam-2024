using JustGame.Script.Manager;
using UnityEngine;

namespace JustGame.Script.Obstacle
{
    public class MoveToTarget : MonoBehaviour
    {
        [SerializeField] private Transform m_target;
        [SerializeField] private float m_moveSpeed;

        private BoxCollider2D m_collider2D;
        private bool m_canMove;
        private Vector2 m_direction;

        private void Start()
        {
            m_collider2D = GetComponent<BoxCollider2D>();
            m_direction = (m_target.position - transform.position).normalized;
        }

        public void Move()
        {
            m_canMove = true;
        }

        private void Update()
        {
            if (!m_canMove) return;

            if (IsHitBlock())
            {
                m_canMove = false;
            }
            
            transform.position = Vector2.MoveTowards(transform.position, 
                    m_target.position, 
                    Time.deltaTime * m_moveSpeed);
        }

        private bool IsHitBlock()
        {
            var hit = Physics2D.OverlapBox((Vector2)transform.position + m_direction * 0.1f, 
                m_collider2D.size, 
                0, 
                LayerManager.BlockMask);
            if (hit != null)
            {
                return true;
            }
            return false;
        }
    }
}
