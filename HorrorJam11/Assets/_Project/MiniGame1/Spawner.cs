using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_4 : MonoBehaviour
{
    [Header("Setup")]
    public Transform[] spawners;
    public GameObject bullet1_prefab;
    [Space]
    [Header("Difficult parameters")]
    public float spawnInterval;
    public float rotationSpeed;


    private float timer;

    void Start()
    {
        timer = 0;
    }

    void FixedUpdate()
    {
        //Spawn control
        timer += Time.fixedDeltaTime;
        if(timer >= spawnInterval)
        {
            timer = 0;
            foreach (Transform spawner in spawners)
            {
                Instantiate(bullet1_prefab, spawner.transform.position, spawner.transform.rotation);
            }
        }

        //Rotation control
        transform.Rotate(transform.forward, rotationSpeed * Time.fixedDeltaTime);
    }


}
