using JustGame.Script.Manager;
using UnityEngine;

namespace JustGame.Script.Obstacle
{
    public class PlaySoundBasedOnDistanceToPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource m_soundPlayer;
        [SerializeField] private float m_minDistance;
        [SerializeField] private float m_maxDistance;

        private float m_dist;
        private Transform m_player;

        public void SetPlayerRef(Transform player)
        {
            m_player = player;
        }
        
        private void Start()
        {
            m_soundPlayer.Play();
            m_soundPlayer.volume = 0;
        }

        private void Update()
        {
            if (m_player == null)
            {
                return;
            }
            m_dist = Vector2.Distance(m_player.position, transform.position);
            m_soundPlayer.volume = 1- MathHelpers.Remap(
                m_dist < m_minDistance ? m_minDistance : m_dist,
                m_minDistance, m_maxDistance,
                0, 1);
        }
    }
}

