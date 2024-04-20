using UnityEngine;
using Random = UnityEngine.Random;

public class TriangleMovement : MonoBehaviour
{
    [SerializeField] private Vector2 m_limit;
    [SerializeField] private float m_moveSpeed;
    [SerializeField] private float m_minSpeed;
    [SerializeField] private float m_maxSpeed;
    [SerializeField] private float m_minDelayChangeSpeed;
    [SerializeField] private float m_maxDelayChangeSpeed;
    [SerializeField] private float m_minDelayChangeDirection;
    [SerializeField] private float m_maxDelayChangeDirection;

    private float m_timerChangeSpeed;
    private float m_timerChangeDirection;
    private Vector2 m_lastPos;
    
    private void Start()
    {
        m_moveSpeed = Random.Range(m_minSpeed, m_maxSpeed);
        m_timerChangeSpeed = Random.Range(m_minDelayChangeSpeed, m_maxDelayChangeSpeed);
        m_timerChangeDirection = Random.Range(m_minDelayChangeDirection, m_maxDelayChangeDirection);
    }

    private void Update()
    {
        m_timerChangeSpeed -= Time.deltaTime;
        m_timerChangeDirection -= Time.deltaTime;

        if (m_timerChangeSpeed <= 0)
        {
            m_moveSpeed = Random.Range(m_minSpeed, m_maxSpeed);
            m_timerChangeSpeed = Random.Range(m_minDelayChangeSpeed, m_maxDelayChangeSpeed);
        }
        
        if (m_timerChangeDirection <= 0)
        {
            m_timerChangeDirection = Random.Range(m_minDelayChangeDirection, m_maxDelayChangeDirection);
            transform.rotation = Quaternion.AngleAxis(Random.Range(0, 360) - 90, Vector3.forward);
        }
        
        transform.Translate(Vector2.up * (Time.deltaTime * m_moveSpeed));
        
        m_lastPos = transform.position;
        if (Mathf.Abs(m_lastPos.x) > m_limit.x)
        {
            m_lastPos.x = m_lastPos.x > 0 ? m_limit.x : -m_limit.x;
            transform.position = m_lastPos;
            
            m_timerChangeDirection = Random.Range(m_minDelayChangeDirection, m_maxDelayChangeDirection);
            transform.rotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward);
        }
        if (Mathf.Abs(m_lastPos.y) > m_limit.y)
        {
            m_lastPos.y = m_lastPos.y > 0 ? m_limit.y : -m_limit.y;
            transform.position = m_lastPos;
            
            m_timerChangeDirection = Random.Range(m_minDelayChangeDirection, m_maxDelayChangeDirection);
            transform.rotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward);
        }
    }
}
