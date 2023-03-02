using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerSprite : MonoBehaviour
{
    private Animator _playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        _playerAnimator = FindObjectOfType<CharacterController>().GetComponent<Animator>();
        _playerAnimator.SetLayerWeight(_playerAnimator.GetLayerIndex("Base Layer"), 0f);
        _playerAnimator.SetLayerWeight(_playerAnimator.GetLayerIndex("Enemy Layer"), 1f);
    }


}
