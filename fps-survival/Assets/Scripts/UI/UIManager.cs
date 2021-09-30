using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class UIManager : NetworkBehaviour
{
    public static UIManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else instance = this;
    }


    public bool isMenuOpen;

    public void ToggleMenu(GameObject UIToToggle)
    {
        UIToToggle.SetActive(!UIToToggle.activeSelf);
        isMenuOpen = UIToToggle.activeSelf;
        ShowCursor();
    }
    //This needs to be simplified 

    

    void ShowCursor()
    {
        if (isMenuOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
