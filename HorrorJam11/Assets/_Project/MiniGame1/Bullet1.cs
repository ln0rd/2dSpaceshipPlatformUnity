using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet1 : MonoBehaviour
{
    public float speed;
    public float lifespan;

    
    private float existingTime;

    public static event Action HitPlayer;

    private void Start()
    {
        existingTime = 0;
    }
    void FixedUpdate()
    {
        transform.Translate(0, speed * Time.fixedDeltaTime, 0, Space.Self);
        
        
        existingTime += Time.fixedDeltaTime;
        if(existingTime >= lifespan)
        {
            Destroy(gameObject); //No object pooling? Shame, but there is no time.
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == 6)
        {
            HitPlayer?.Invoke();
            Destroy(gameObject);
        }
    }
}
