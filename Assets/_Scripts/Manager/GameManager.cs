using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JustGame.Script.Manager;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Vector2 m_limit;
    [SerializeField] private GameObject m_playerPrefab;
    [SerializeField] private GameObject m_enterDoorPrefab;
    [SerializeField] private GameObject m_exitDoorPrefab;
    [SerializeField] private BoolEvent m_levelWinEvent;
    [SerializeField] private ActionEvent m_loadNewLevel;
    [SerializeField] private ActionEvent m_playOpeningEvent;
    [SerializeField] private ActionEvent m_finalLevelEvent;
    [SerializeField] private AudioSource m_music;
    [SerializeField] private AudioSource m_exitDoorSound;
    [Header("Level")] 
    [SerializeField] private int m_curLevel;
    [SerializeField] private GameObject[] m_levelPrebabList;
    [SerializeField] private List<GameObject> m_objectInLevelList;

    private int m_lastHealth;
    private int m_lastMin;
    private int m_lastSec;
    private int m_maxLevel;
    private Vector2 m_enterPos;
    private bool m_isFinalLevel;
    
    
    public int LastMinute => m_lastMin;
    public int LastSeconds => m_lastSec;

    public int FloorNumber => m_maxLevel + 1 - m_curLevel;

    public Vector2 GlobalLimit => m_limit;

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => !SceneLoader.Instance.IsLoadDone);
        
        m_maxLevel = m_levelPrebabList.Length;
        m_objectInLevelList = new List<GameObject>();
        m_curLevel = 1;
        m_lastHealth = 3;
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

    public void PlayExitDoorSound()
    {
        m_exitDoorSound.Play();
    }
    
    public void PlayBGM()
    {
        if (m_music.isPlaying) return;
        m_music.Play();
        m_music.DOFade(1, 1f);
    }

    public void StopBGM()
    {
        m_music.DOFade(0, 0.5f).OnComplete(() =>
        {
            m_music.Stop();
        });
    }
    
    private void OnLevelWon(bool isWin)
    {
        Pause();
        
        for (int i = 0; i < m_objectInLevelList.Count; i++)
        {
            if (m_objectInLevelList[i].TryGetComponent<Health>(out var health))
            {
                m_lastHealth = health.CurrentHealth;
            }
            Destroy(m_objectInLevelList[i]);
        }
        
        if (isWin && m_isFinalLevel)
        {
            //TODO: What do I need to do for final moment ?
            StopBGM();
            m_finalLevelEvent.Raise();
            return;
        }

        
        if (isWin)
        {
            m_curLevel++;
            LoadLevel(1.5f);
        }
    }

    public void Replay()
    {
        //Force to set player health back to 3
        LoadLevel(0, 3);
    }
    
    public void LoadLevel(float delay = 0, int forceHealth = 0)
    {
        StartCoroutine(LoadLevelRoutine(delay, 3));
    }

    private IEnumerator LoadLevelRoutine(float delay  = 0,int forceHealth = 0)
    {
        Pause();
        m_objectInLevelList.Clear();

        yield return new WaitForSecondsRealtime(delay);
        
        m_playOpeningEvent.Raise();

        yield return new WaitForSecondsRealtime(0.3f+0.3f+0.7f);
        
        LoadLevelLayout(m_curLevel);
        yield return new WaitForSecondsRealtime(0.5f);
        var player = Instantiate(m_playerPrefab,m_enterPos, Quaternion.identity);

        var health = player.GetComponent<Health>();
        if (forceHealth > 0)
        {
            health.SetHealth(forceHealth);
            m_lastHealth = forceHealth;
        }
        else
        {
            health.SetHealth(m_lastHealth);
        }
        
        
        m_objectInLevelList.Add(player);
        PlayBGM();
        m_loadNewLevel.Raise();
        UnPause();
    }
    

    private void LoadLevelLayout(int level)
    {
        var levelGO = Instantiate(m_levelPrebabList[level-1],Vector3.zero,Quaternion.identity);
        var levelData = levelGO.GetComponent<LevelData>();
        if (levelData is FinalLevel)
        {
            m_isFinalLevel = true;
        }
        
        m_enterPos = levelData.EnterDoor.position;
        
        m_objectInLevelList.Add(levelGO);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position,m_limit * 2);
    }
}
