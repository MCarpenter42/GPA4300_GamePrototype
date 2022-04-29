using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class PauseMenu : UI
{
    #region [ PARAMETERS ]

    private FrameHandler frameHandler;

    [SerializeField] bool closeAllNonMenu;

    #endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    void Awake()
    {
        frameHandler = gameObject.AddComponent<FrameHandler>();
        frameHandler.SetValues(true);
        frameHandler.GetComponents(gameObject);
        frameHandler.onShow = OnShow;
    }

    void Start()
    {
        ShowMenu(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(controls.menu.pause) && !GameManager.state.gameEnded == true)
        {
            OnPausePress();
        }
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    private void OnPausePress()
    {
        if (GameManager.frameController.GetOpenCount() > 0)
        {
            if (closeAllNonMenu)
            {
                GameManager.frameController.CloseAll();
            }
            else
            {
                GameManager.frameController.CloseLast();
            }
        }
        else
        {
            ToggleMenu();
        }
    }

    public void ShowMenu(bool show)
    {
        frameHandler.Show(show);
    }

    public void ToggleMenu()
    {
        frameHandler.Toggle();
    }

    private void OnShow(bool show)
    {
        if (show)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }

    public void ChangeMenuFrame(int index)
    {
        frameHandler.ChangeFrame(index);
    }

    public void ReturntoStartMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void ExitGame()
    {
        Debug.Log("Game Closed!");
        Application.Quit();
    }


}
