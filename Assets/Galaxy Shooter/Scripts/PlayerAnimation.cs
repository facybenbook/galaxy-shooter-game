using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    private Animator _playerAnimator;

	// Use this for initialization
	void Start () {
        _playerAnimator = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxis("Mouse X") == -1)
        {
            _playerAnimator.SetBool("Turn_Left", true);
            _playerAnimator.SetBool("Turn_Right", false);
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _playerAnimator.SetBool("Turn_Left", false);
            _playerAnimator.SetBool("Turn_Right", false);
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetAxis("Mouse X") == 1)
        {
            _playerAnimator.SetBool("Turn_Left", false);
            _playerAnimator.SetBool("Turn_Right", true);
        }
        else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            _playerAnimator.SetBool("Turn_Left", false);
            _playerAnimator.SetBool("Turn_Right", false);
        }

    }
}
