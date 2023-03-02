using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastEnemyTrigger : MonoBehaviour
{
    public GameObject TriggerToReview;

    private void OnEnable()
    {
        MiniGamesManager.MiniGameWon += ReviewTrigger;
    }

    private void OnDisable()
    {
        MiniGamesManager.MiniGameWon -= ReviewTrigger;
    }

    
    private void ReviewTrigger()
    {
        TriggerToReview.SetActive(true);
    }


}
