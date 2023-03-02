using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(EnemyDetector), typeof(AudioSource))]
public class FearController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public AudioSource HearthBeatSound;

    private float fear;
    private EnemyDetector detector;
    private PlayerSettings _playerSettings;
    private GameStates currentGameState;


    private void OnEnable()
    {
        GameManager.StateChangedOrSceneLoaded += StartMiniGameSetup;
    }

    private void OnDisable()
    {
        GameManager.StateChangedOrSceneLoaded -= StartMiniGameSetup;

    }

    private void Start()
    {
        detector = GetComponent<EnemyDetector>();
        HearthBeatSound.Play();
        fear = 0;
        _playerSettings = FindObjectOfType<GameManager>().PlayerSettings;
    }


    private void FixedUpdate()
    {
        if (currentGameState != GameStates.MiniGame)
        {

            if (detector.GetEnemiesDetectedSumMeters() > 0.1f)
            {
                fear = 1f - (detector.GetEnemiesDetectedSumMeters() / (_playerSettings.FearThreshold * detector.GetEnemiesQtyDetected()));
            }
            else
            {
                fear = 0;
            }
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = Mathf.Clamp(fear * 3, 0, 2f);
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = Mathf.Clamp(fear * 3, 0, 2f);

            HearthBeatSound.volume = Mathf.Clamp(fear * 3, 0, 2f);
            HearthBeatSound.pitch = Mathf.Clamp(fear * 3, 0, 2f);
        }




    }


    private void StartMiniGameSetup(GameStates gameState)
    {
        if (gameState == GameStates.MiniGame)
        {
            HearthBeatSound.Stop();

        }
    }

}
