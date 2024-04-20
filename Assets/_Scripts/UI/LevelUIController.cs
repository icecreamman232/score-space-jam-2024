using JustGame.Scripts.ScriptableEvent;
using TMPro;
using UnityEngine;

public class LevelUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_levelTxt;
    [SerializeField] private ActionEvent m_OnLoadLevelDoneEvent;

    private void Start()
    {
        m_OnLoadLevelDoneEvent.AddListener(OnLoadLevelDone);
    }

    private void OnDestroy()
    {
        m_OnLoadLevelDoneEvent.RemoveListener(OnLoadLevelDone);
    }

    private void OnLoadLevelDone()
    {
        m_levelTxt.text = $"Floor {(4 - GameManager.Instance.CurrentLevel)}";
    }
}
