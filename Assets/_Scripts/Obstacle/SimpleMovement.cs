using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed;
    [SerializeField] private Vector2[] m_moveDirection;

    private Vector2 m_limit;
    private Vector2 m_direction;
    private Vector2 m_lastPos;
    private int m_indexDirArr;

    private void Start()
    {
        m_indexDirArr = 0;
        m_direction = m_moveDirection[m_indexDirArr];
        var boxCollider2D = GetComponent<BoxCollider2D>();
        m_limit = GameManager.Instance.GlobalLimit - (Vector2)boxCollider2D.bounds.extents;
    }

    private void Update()
    {
        transform.Translate(m_direction *(Time.deltaTime * m_moveSpeed));
        m_lastPos = transform.position;
        if (Mathf.Abs(m_lastPos.x) > m_limit.x)
        {
            m_lastPos.x = m_lastPos.x > 0 ? m_limit.x : -m_limit.x;
            transform.position = m_lastPos;

            m_indexDirArr = m_indexDirArr == 0 ? 1 : 0;
            m_direction = m_moveDirection[m_indexDirArr];
        }
        if (Mathf.Abs(m_lastPos.y) > m_limit.y)
        {
            m_lastPos.y = m_lastPos.y > 0 ? m_limit.y : -m_limit.y;
            transform.position = m_lastPos;
            m_indexDirArr = m_indexDirArr == 0 ? 1 : 0;
            m_direction = m_moveDirection[m_indexDirArr];
        }
    }
}
