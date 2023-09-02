using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            //restart the current scene
            //Application.LoadLevel(1); //only works in unity 4 and 5
            SceneManager.LoadScene(1); //works in unity 5 and 2017
        }

        if (Input.GetKeyDown(KeyCode.Escape) && _isGameOver == true)
        {
            Application.Quit();
        }
    }
    public void GameOver()
    {
        _isGameOver = true;
    }
}
