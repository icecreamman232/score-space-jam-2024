using System;
using DG.Tweening;
using JustGame.Script.Manager;
using JustGame.Scripts.Attribute;
using JustGame.Scripts.Common;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_movespeed;
    [SerializeField] private Vector2 m_limit;
    [SerializeField] [ReadOnly] private Vector2 m_direction;
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private AnimationParameter m_spinAnim;
    private bool m_canMove;
    private Vector2 m_lastPos;
    private BoxCollider2D m_collider2D;
    
    public void PauseMoving()
    {
        m_canMove = false;
        m_direction = Vector2.zero;
    }

    public void UnpauseMoving()
    {
        m_canMove = true;
    }

    public void MoveToExitDoor(Transform target)
    {
        m_spinAnim.SetTrigger();
        transform.DOMove(target.position, m_spinAnim.Duration).SetUpdate(true);
    }
    

    private void Start()
    {
        UnpauseMoving();
        m_collider2D = GetComponent<BoxCollider2D>();
        var globalLimit = GameManager.Instance.GlobalLimit;
        m_limit = globalLimit - (Vector2)m_collider2D.bounds.extents;
    }

    private void Update()
    {
        if (!m_canMove) return;
        HandleInput();
        Movement();
    }

    private void HandleInput()
    {
        m_direction = Vector2.zero;
            
        if (Input.GetKey(KeyCode.A))
        {
            m_direction.x = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            m_direction.x = 1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            m_direction.y = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            m_direction.y = -1;
        }

        if (m_direction.x != 0)
        {
            m_spriteRenderer.flipX = m_direction.x > 0;
        }
    }

    private void Movement()
    {
        if (IsThereObstacle())
        {
            return;
        }
        
        transform.Translate(m_direction * (m_movespeed * Time.deltaTime));
        m_lastPos = transform.position;
        if (Mathf.Abs(m_lastPos.x) > m_limit.x)
        {
            m_lastPos.x = m_lastPos.x > 0 ? m_limit.x : -m_limit.x;
            transform.position = m_lastPos;
        }
        if (Mathf.Abs(m_lastPos.y) > m_limit.y)
        {
            m_lastPos.y = m_lastPos.y > 0 ? m_limit.y : -m_limit.y;
            transform.position = m_lastPos;
        }
    }

    private bool IsThereObstacle()
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube((Vector2)transform.position + m_direction * 0.1f, m_collider2D.size );
    }
}
