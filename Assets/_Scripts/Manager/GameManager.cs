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
    [SerializeField] private int m_maxLevel;
    [SerializeField] private int m_curLevel;
    [SerializeField] private GameObject[] m_levelPrebabList;
    [SerializeField] private List<GameObject> m_objectInLevelList;
    
    private int m_lastMin;
    private int m_lastSec;
    
    private Vector2 m_enterPos;

    public int LastMinute => m_lastMin;
    public int LastSeconds => m_lastSec;

    public int FloorNumber => m_maxLevel + 1 - m_curLevel;

    public Vector2 GlobalLimit => m_limit;

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
        
        LoadLevelLayout(m_curLevel);
        yield return new WaitForSecondsRealtime(0.5f);
        var player = Instantiate(m_playerPrefab,m_enterPos, Quaternion.identity);
        m_objectInLevelList.Add(player);
        m_loadNewLevel.Raise();
        UnPause();
    }
    

    private void LoadLevelLayout(int level)
    {
        var levelGO = Instantiate(m_levelPrebabList[level-1],Vector3.zero,Quaternion.identity);
        var levelData = levelGO.GetComponent<LevelData>();
        m_enterPos = levelData.EnterDoor.position;
        
        m_objectInLevelList.Add(levelGO);
    }

    private Vector2 RandomPosInLimit(Vector2 limit)
    {
        var pos = Vector2.zero;
        pos.x = Random.Range(-limit.x, limit.x);
        pos.y = Random.Range(-limit.y, limit.y);
        return pos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position,m_limit * 2);
    }
}
