using System;
using DG.Tweening;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private DOTweenAnimation m_shakeTween;
    [SerializeField] private ActionEvent m_cameraShakeEvent;

    private void Start()
    {
        m_cameraShakeEvent.AddListener(OnCameraShake);
    }

    private void OnDestroy()
    {
        m_cameraShakeEvent.RemoveListener(OnCameraShake);
    }

    [ContextMenu("Test Shake")]
    private void OnCameraShake()
    {
        m_shakeTween.DORestart();
    }
}
