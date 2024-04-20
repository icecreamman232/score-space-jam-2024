using UnityEngine;

public class TriangleBullet : MonoBehaviour
{
    [SerializeField] private Vector2 m_limit;
    [SerializeField] private float m_moveSpeed;

    private bool m_canUpdate;
    private Vector2 m_lastPos;
    public void SpawnProjectile(Vector3 position, Quaternion quaternion = default)
    {
        transform.position = position;
        transform.rotation = quaternion;
        m_canUpdate = true;
    }

    private void Update()
    {
        if (!m_canUpdate) return;
        transform.Translate(Vector3.up * (Time.deltaTime * m_moveSpeed));
        m_lastPos = transform.position;
        if (Mathf.Abs(m_lastPos.x) > m_limit.x || Mathf.Abs(m_lastPos.y) > m_limit.y)
        {
            this.gameObject.SetActive(false);
            m_canUpdate = false;
        }
    }
}
