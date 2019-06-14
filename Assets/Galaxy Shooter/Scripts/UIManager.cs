using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Sprite[] liveImages;

    public Image livesImageDisplay;

    public GameObject titleScreen;

    public Text scoreText;

    public int score;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateLives(int currentLives)
    {
        Debug.Log("Player lives: " + currentLives);
        livesImageDisplay.sprite = liveImages[currentLives];

    }

    public void UpdateScore()
    {
        score += 10;
        scoreText.text = "Score: " + score;

    }

    public void ShowTitleScreen ()
    {
        titleScreen.SetActive(true);
    }

    public void HideTitleScreen()
    {
        titleScreen.SetActive(false);
        score = 0;
        scoreText.text = "Score: " + score;
        
    }

}
