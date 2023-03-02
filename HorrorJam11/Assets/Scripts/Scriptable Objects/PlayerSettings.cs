using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerSettings",menuName ="_PlayerSettings")]
public class PlayerSettings : ScriptableObject
{
    [Header("Player Settings")]
    public float EnemyDetectionRange;
    public int EnemiesToConsiderForCalculation;
    public float FearThreshold;
    public float WalkSpeed;

    [Space]
    [Header("Enemy Settings")]
    public float EnemySpeedAlerted;
    public float EnemySpeedChase;
    public float MaxRangePlayerDetection;
    public float ChaseTheresholdRangePlayerDetection;

}
