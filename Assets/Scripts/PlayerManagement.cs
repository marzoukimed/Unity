using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManagement : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;
    public static bool isGameStarted;
    public GameObject startingText;
    public GameObject startingImage;
    public GameObject pauseMenuScreen;
    public GameObject pauseImage;

    public static int numberCoins;
    public Text CoinsText;
    public Text CoinsScore;
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        isGameStarted = false;
        numberCoins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            Destroy(pauseImage);
            CoinsScore.text = "" + numberCoins;
        }

        CoinsText.text = ":" + numberCoins;
        if(SwipeManager.tap)
        {
            isGameStarted = true;
            Destroy(startingText);
            Destroy(startingImage);
            
        }
    }


    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenuScreen.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenuScreen.SetActive(false);
    }
}
