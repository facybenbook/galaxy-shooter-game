using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public bool gameOver = true;
    public GameObject player;

    private UIManager _UIManager;

	// Use this for initialization
	void Start () {
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
	}
	
	// Update is called once per frame
	void Update () {

        if (gameOver)
        {

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                Instantiate(player, Vector3.zero, Quaternion.identity);
                gameOver = false;
                _UIManager.HideTitleScreen();
            }
        }

        }
}
