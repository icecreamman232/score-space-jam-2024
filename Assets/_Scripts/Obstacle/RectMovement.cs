using UnityEngine;
using Random = UnityEngine.Random;

public class RectMovement : MonoBehaviour
{
    [SerializeField] private Vector2[] m_limitArr;
    [SerializeField] private Vector2 m_limit;
    [SerializeField] private float m_minDuration;
    [SerializeField] private float m_maxDuration;
    [Header("Speed")] 
    [SerializeField] private float m_moveSpeed;
    [SerializeField] private float m_minSpeed;
    [SerializeField] private float m_maxSpeed;
    [SerializeField] private float m_minDurationChangeSpeed;
    [SerializeField] private float m_maxDurationChangeSpeed;

    private Vector2 m_direction;
    private float m_timer;
    private float m_timerChangeSpeed;
    private Vector2 m_lastPos;

    private Vector2[] m_directionArr =
    {
        new(-1, 0),
        new(-1, -1),
        new(-1, 1),
        
        new(1, 0),
        new(1, -1),
        new(1, 1),
    };

    private void Start()
    {
        m_timer = GetRandomDuration();
        ChangeDirection();
        
        m_timerChangeSpeed = GetRandomChangeSpeedDuration();
        m_moveSpeed = GetRandomSpeed();

        transform.localScale = GetRandomScale();
        SetLimit(transform.localScale.x);
    }

    private void Update()
    {
        m_timer -= Time.deltaTime;
        m_timerChangeSpeed -= Time.deltaTime;
        if (m_timer <= 0)
        {
            m_timer = GetRandomDuration();
            ChangeDirection();
        }

        if (m_timerChangeSpeed <= 0)
        {
            m_timerChangeSpeed = GetRandomChangeSpeedDuration();
            m_moveSpeed = GetRandomSpeed();
        }

        Movement();
    }

    private void Movement()
    {
        transform.Translate(m_direction *(Time.deltaTime * m_moveSpeed));
        m_lastPos = transform.position;
        if (Mathf.Abs(m_lastPos.x) > m_limit.x)
        {
            m_lastPos.x = m_lastPos.x > 0 ? m_limit.x : -m_limit.x;
            transform.position = m_lastPos;
            ChangeDirection();
        }
        if (Mathf.Abs(m_lastPos.y) > m_limit.y)
        {
            m_lastPos.y = m_lastPos.y > 0 ? m_limit.y : -m_limit.y;
            transform.position = m_lastPos;
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        m_direction = m_directionArr[Random.Range(0, 6)];
    }

    private void SetLimit(float scaleValue)
    {
        switch (scaleValue)
        {
            case 1:
                m_limit = m_limitArr[0];
                break;
            case 2:
                m_limit = m_limitArr[1];
                break;
            case 3:
                m_limit = m_limitArr[2];
                break;
        }
    }

    private Vector3 GetRandomScale()
    {
        return Vector3.one * Random.Range(1, 4);
    }

    private float GetRandomSpeed()
    {
        return Random.Range(m_minSpeed, m_maxSpeed);
    }

    private float GetRandomChangeSpeedDuration()
    {
        return Random.Range(m_minDurationChangeSpeed, m_maxDurationChangeSpeed);
    }

    private float GetRandomDuration()
    {
        return Random.Range(m_minDuration, m_maxDuration);
    }
}
