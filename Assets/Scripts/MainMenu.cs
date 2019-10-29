using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject Controls;
    private bool active = false;
    // Start is called before the first frame update
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void control()
    {
        if (!active)
        {
            Controls.SetActive(true);
            active = !active;
        }
        else
        {
            Controls.SetActive(false);
            active = !active;
        }
        
    }

    public void QuitGame()
    {
        Debug.Log("You quit.");
        Application.Quit();
    }

}
