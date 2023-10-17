using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    GameObject pausePanel;

    bool isPause = false;

    public void TryPause()
    {
        isPause = !isPause;
        GameManager.isPause = isPause;

        pausePanel.SetActive(isPause);

        if (isPause)
            OpenMenu();
        else
            CloseMenu();
    }

    void OpenMenu()
    {
        Time.timeScale = 0;
    }

    void CloseMenu()
    {
        Time.timeScale = 1;
    }
}
