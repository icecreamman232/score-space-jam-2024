using UnityEngine;

namespace JustGame.Script.Data
{
    [CreateAssetMenu(menuName = "JustGame/Level Container")]
    public class LevelContainer : ScriptableObject
    {
        [SerializeField] private GameObject[] m_levelList;

        public GameObject GetLevel(int level) => m_levelList[level];

        public int LevelNumber => m_levelList.Length;
    }
}

