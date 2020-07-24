using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    
    public static bool IsPaused = false;
    public GameObject pauseUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        
        {
            if (IsPaused)
            {
                Resume();
            }
            else 
            {

                Pause();
            }
        

            void Resume() 
            {
                pauseUI.SetActive(false);
                Time.timeScale = 1f;
                IsPaused = false;
            
            
            }

            void Pause() 
            
            {
                pauseUI.SetActive(true);
                Time.timeScale = 0f;
                IsPaused = true;
            
            }
        }
    }
}
