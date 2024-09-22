using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField]
    private bool _isgameOver;

    private void Update()
    {
     
        if(Input.GetKeyDown(KeyCode.R) && _isgameOver == true)
        {
            SceneManager.LoadScene(1); //momentane Game Scene (File -> buidlSetting -> Add open Scene)
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        
    }

    public void GameOver()
    {
        _isgameOver = true;
    }
}
