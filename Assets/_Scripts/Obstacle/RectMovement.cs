using UnityEngine;
using Random = UnityEngine.Random;

public class RectMovement : MonoBehaviour
{
    [SerializeField] private Vector2 m_limit;
    [SerializeField] private float m_minDuration;
    [SerializeField] private float m_maxDuration;
    [Header("Direction")]
    [SerializeField] private bool m_is4Direction;
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

    private Vector2[] m_8directionArr =
    {
        new(0, 1), //top
        new(1, 1), //top-right
        new(1, 0), //right
        
        new(1, -1), //bot-right
        new(0, -1), //bot
        new(-1, -1), //bot-left
        
        new(-1, 0), //left
        new(-1, 1), //top-left
    };
    
    private Vector2[] m_4directionArr =
    {
        new(0, 1), //top
        new(1, 0), //right
        new(0, -1), //bot
        new(-1, 0), //left
    };

    private void Start()
    {
        m_timer = GetRandomDuration();
        ChangeDirection();
        
        m_timerChangeSpeed = GetRandomChangeSpeedDuration();
        m_moveSpeed = GetRandomSpeed();

        var boxCollider2D = GetComponent<BoxCollider2D>();

        m_limit = GameManager.Instance.GlobalLimit - (Vector2)boxCollider2D.bounds.extents;
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
        if (m_is4Direction)
        {
            m_direction = m_4directionArr[Random.Range(0, m_4directionArr.Length)];
        }
        else
        {
            m_direction = m_8directionArr[Random.Range(0, m_8directionArr.Length)];
        }
        
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
