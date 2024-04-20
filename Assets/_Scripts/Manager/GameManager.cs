using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Vector2 m_limit;
    [SerializeField] private GameObject m_playerPrefab;
    [SerializeField] private GameObject m_enterDoorPrefab;
    [SerializeField] private GameObject m_exitDoorPrefab;
        
    
    private Vector2 m_enterPos;
    private Vector2 m_exitPos;

    private IEnumerator Start()
    {
        m_enterPos = RandomPosInLimit(m_limit);
        var isOvelapped = true;
        while (isOvelapped)
        {
            m_exitPos = RandomPosInLimit(m_limit);
            if (Vector2.Distance(m_enterPos,m_exitPos) >= 5)
            {
                isOvelapped = false;
            }
        }

        Instantiate(m_enterDoorPrefab, m_enterPos, Quaternion.identity);
        Instantiate(m_exitDoorPrefab, m_exitPos, Quaternion.identity);

        yield return new WaitForSeconds(0.5f);
        
        Instantiate(m_playerPrefab,m_enterPos, Quaternion.identity);
    }

    private Vector2 RandomPosInLimit(Vector2 limit)
    {
        var pos = Vector2.zero;
        pos.x = Random.Range(-limit.x, limit.x);
        pos.y = Random.Range(-limit.y, limit.y);
        return pos;
    }
}
