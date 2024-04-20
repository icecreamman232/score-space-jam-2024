using System;
using JustGame.Scripts.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

public class TriangleGun : MonoBehaviour
{
    [SerializeField] private ObjectPooler m_bulletPooler;
    [SerializeField] private Transform m_shootPivot;
    [SerializeField] private float m_minDelayShot;
    [SerializeField] private float m_maxDelayShot;
    [SerializeField] private int m_bulletPerShot;

    private float m_timer;

    private void Start()
    {
        m_timer = Random.Range(m_minDelayShot, m_maxDelayShot);
    }

    private void Update()
    {
        m_timer -= Time.deltaTime;
        
        if (m_timer <= 0)
        {
            m_timer = Random.Range(m_minDelayShot, m_maxDelayShot);;
            Shoot();
        }
    }

    private void Shoot()
    {
        for (int i = 0; i < m_bulletPerShot; i++)
        {
            var bulletGO = m_bulletPooler.GetPooledGameObject();
            var bullet = bulletGO.GetComponent<TriangleBullet>();
            if (bullet == null) return;
            bullet.SpawnProjectile(m_shootPivot.position,Quaternion.AngleAxis(Random.Range(0,360),Vector3.forward));
        }
    }
}
