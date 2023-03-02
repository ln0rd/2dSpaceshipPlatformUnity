using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalLogRevelator : MonoBehaviour
{

    public GameObject FinalLog;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            FinalLog.SetActive(true);
        }
    }
}
