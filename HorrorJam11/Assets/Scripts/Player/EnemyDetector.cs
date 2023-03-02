using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class EnemyDetector : MonoBehaviour
{
    private Enemy[] _enemiesInScene;
    private List<Enemy> _detectedEnemiesByDistance = new List<Enemy>();
    private RaycastHit2D _detectionRayHit;
    private LayerMask _layerMask;
    private float SumOfnearEnemies;
    private PlayerSettings _playerSettings;

    public float detectionRange;

    void Start()
    {
        _enemiesInScene = Object.FindObjectsOfType<Enemy>().OrderBy(t => (t.transform.position - transform.position).sqrMagnitude).ToArray();
        _layerMask = LayerMask.GetMask("Default", "Enemy");
        _playerSettings = FindObjectOfType<GameManager>().PlayerSettings;
    }

    void FixedUpdate()
    {
        SumOfnearEnemies = 0;
        _detectedEnemiesByDistance.Clear();

        _enemiesInScene = Object.FindObjectsOfType<Enemy>().OrderBy(t => (t.transform.position - transform.position).sqrMagnitude).Take(_playerSettings.EnemiesToConsiderForCalculation).ToArray();   //10-15 inimigos na cena. Se precisar, melhorar depois usando collider. Por hora Linq vai resolver.
                                                                                                                                                                                                      //Vou pegar 3 para caso o mais perto estiver escondido, ainda assim o efeito do medo disparar caso tenha um segundo inimigo mais longe, porém na reta de detecção.


        foreach (Enemy enemy in _enemiesInScene)
        {
            Vector3 direction = enemy.transform.position - transform.position;

            Debug.DrawRay(transform.position, direction);

            _detectionRayHit = Physics2D.Raycast(transform.position, direction, detectionRange, _layerMask, -1, 1);

            if (_detectionRayHit && _detectionRayHit.transform.gameObject.layer == 7)
            {
                if (_detectionRayHit.distance <= _playerSettings.FearThreshold)
                {
                    SumOfnearEnemies += _detectionRayHit.distance;
                    SumOfnearEnemies = Mathf.Clamp(SumOfnearEnemies, 0, _playerSettings.FearThreshold);
                }
                else
                {
                    SumOfnearEnemies += 0;
                }
                _detectedEnemiesByDistance.Add(enemy);
            }


        }

    }

    public float GetEnemiesDetectedSumMeters()
    {
        return SumOfnearEnemies;
    }

    public int GetEnemiesQtyDetected()
    {
        return _detectedEnemiesByDistance.Count;
    }

}
