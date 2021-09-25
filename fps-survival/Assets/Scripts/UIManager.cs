using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
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

    [SerializeField] Transform allPlayerInventoryUI;
    [SerializeField] Transform playeritemsInventoryUI;
    [SerializeField] Transform storageInventoryUI;
    [SerializeField] Transform builderUI;

    public bool isMenuOpen;

    //This needs to be simplified 

    public void TogglePlayerInventory_ALL()
    {
        allPlayerInventoryUI.gameObject.SetActive(!allPlayerInventoryUI.gameObject.activeSelf);
        isMenuOpen = allPlayerInventoryUI.gameObject.activeSelf;
        ShowCursor();
    }

    public void TogglePlayerInventory_ITEMS()
    {
        playeritemsInventoryUI.gameObject.SetActive(!playeritemsInventoryUI.gameObject.activeSelf);
        isMenuOpen = playeritemsInventoryUI.gameObject.activeSelf;
        ShowCursor();
    }

    public void ToggleStorageInventory()
    {
        storageInventoryUI.gameObject.SetActive(!storageInventoryUI.gameObject.activeSelf);
        playeritemsInventoryUI.gameObject.SetActive(storageInventoryUI.gameObject.activeSelf);
        isMenuOpen = storageInventoryUI.gameObject.activeSelf;
        ShowCursor();
    }

    public void ToggleBuilderUI()
    {
        builderUI.gameObject.SetActive(!builderUI.gameObject.activeSelf);
        isMenuOpen = builderUI.gameObject.activeSelf;
        ShowCursor();
    }

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
