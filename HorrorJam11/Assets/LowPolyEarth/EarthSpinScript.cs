using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class EarthSpinScript : MonoBehaviour {
    public float speed = 10f;

    void Update() {
        transform.Rotate(Vector3.up, speed * Time.deltaTime, Space.Self);
    }
}