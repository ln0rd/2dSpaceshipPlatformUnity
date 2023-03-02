using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Generatorlight : MonoBehaviour
{
    private Light2D genLight;


    // Start is called before the first frame update
    void Start()
    {
        genLight = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        genLight.intensity = Mathf.Clamp((Mathf.Sin(Time.time * 2) * 0.5f + 0.5f) * 2, 0.05f, 1.5f);
    }
}
