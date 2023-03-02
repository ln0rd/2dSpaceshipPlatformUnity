using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDebugDelay : MonoBehaviour
{
    private UIInputManager inputManager;
    void Start()
    {
        inputManager = GetComponent<UIInputManager>();
    }

public IEnumerator DelayToActivate()
    {
        yield return new WaitForSeconds(2f);
        inputManager.ActivateDebugMap();
    }
}
