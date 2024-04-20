using System.Collections;
using System.Collections.Generic;
using JustGame.Script.Manager;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Vector2 m_limit;
    [SerializeField] private GameObject m_playerPrefab;
    [SerializeField] private GameObject m_enterDoorPrefab;
    [SerializeField] private GameObject m_exitDoorPrefab;
    [SerializeField] private BoolEvent m_levelWinEvent;
    [SerializeField] private ActionEvent m_loadNewLevel;
    [Header("Level")] 
    [SerializeField] private int m_curLevel;
    [SerializeField] private GameObject[] m_obstacleLevel_1_List;
    [SerializeField] private GameObject[] m_obstacleLevel_2_List;
    [SerializeField] private GameObject[] m_obstacleLevel_3_List;
    [SerializeField] private List<GameObject> m_objectInLevelList;
    
    private int m_lastMin;
    private int m_lastSec;
    
    private Vector2 m_enterPos;
    private Vector2 m_exitPos;

    public int LastMinute => m_lastMin;
    public int LastSeconds => m_lastSec;

    private void Start()
    {
        m_objectInLevelList = new List<GameObject>();
        m_curLevel = 1;
        m_levelWinEvent.AddListener(OnLevelWon);

        //LOAD LEVEL ROUTINE START HERE
        LoadLevel();
    }

    private void OnDestroy()
    {
        if (m_levelWinEvent == null) return;
        m_levelWinEvent.RemoveListener(OnLevelWon);
    }

    private void Pause()
    {
        Time.timeScale = 0;
    }

    private void UnPause()
    {
        Time.timeScale = 1;
    }

    public void RecordTime(int min, int sec)
    {
        m_lastMin = min;
        m_lastSec = sec;
    }
    
    private void OnLevelWon(bool isWin)
    {
        Pause();
        if (isWin)
        {
            m_curLevel++;
        }
    }

    public void LoadLevel()
    {
        StartCoroutine(LoadLevelRoutine());
    }

    private IEnumerator LoadLevelRoutine()
    {
        Pause();

        for (int i = 0; i < m_objectInLevelList.Count; i++)
        {
            Destroy(m_objectInLevelList[i].gameObject);
            yield return new WaitForEndOfFrame();
        }
        m_objectInLevelList.Clear();
        
        CreateDoors();
        LoadObstacleForLevel(m_curLevel);
        yield return new WaitForSecondsRealtime(0.5f);
        var player = Instantiate(m_playerPrefab,m_enterPos, Quaternion.identity);
        m_objectInLevelList.Add(player);
        m_loadNewLevel.Raise();
        UnPause();
    }
    
    private void CreateDoors()
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
        
        var enterDoor = Instantiate(m_enterDoorPrefab, m_enterPos, Quaternion.identity);
        var exitDoor = Instantiate(m_exitDoorPrefab, m_exitPos, Quaternion.identity);
        m_objectInLevelList.Add(enterDoor);
        m_objectInLevelList.Add(exitDoor);
    }

    private void LoadObstacleForLevel(int level)
    {
        //TODO Add more details on which and how many obstacle should be load into level
        switch (level)
        {
            case 1:
                var obstacle1 = Instantiate(m_obstacleLevel_1_List[Random.Range(0, m_obstacleLevel_1_List.Length)],Vector3.zero,Quaternion.identity);
                m_objectInLevelList.Add(obstacle1);
                break;
            case 2:
                var obstacle2 = Instantiate(m_obstacleLevel_2_List[Random.Range(0, m_obstacleLevel_2_List.Length)],Vector3.zero,Quaternion.identity);
                m_objectInLevelList.Add(obstacle2);
                break;
            case 3:
                var obstacle3=Instantiate(m_obstacleLevel_3_List[Random.Range(0, m_obstacleLevel_3_List.Length)],Vector3.zero,Quaternion.identity);
                m_objectInLevelList.Add(obstacle3);
                break;
        }
    }

    private Vector2 RandomPosInLimit(Vector2 limit)
    {
        var pos = Vector2.zero;
        pos.x = Random.Range(-limit.x, limit.x);
        pos.y = Random.Range(-limit.y, limit.y);
        return pos;
    }
}
