using System;
using UnityEngine;

public class MenuUIController : MonoBehaviour
{
    private bool m_canPress;

    private void Start()
    {
        m_canPress = true;
    }
    
    public void PlayGame()
    {
        if (!m_canPress) return;
        m_canPress = false;
        SceneLoader.Instance.LoadScene("MenuScene","GameplayScene");
    }


    public void ShowLeaderboard()
    {
        if (!m_canPress) return;
    }
}
