using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item",menuName = "Items/Test/New Item")]
public class Item : ScriptableObject
{
   
    [SerializeField] private int _stackSizePlayer = 1;
    [SerializeField] private int _stackSizeGatherer = 1;
    [SerializeField] private int _stackSizeStorage = 1;
    [SerializeField] private int _stackSizeVendor = 1;

    [SerializeField]
    private Sprite _icon;
    public Sprite icon => _icon;

    public int GetStackSize(StorageType storageType)
    {
        switch (storageType)
        {
            case StorageType.Player:
                return _stackSizePlayer;
            case StorageType.Gatherer:
                return _stackSizeGatherer;
            case StorageType.Storage:
                return _stackSizeStorage;
            case StorageType.Vendor:
                return _stackSizeVendor;
            default:
                return _stackSizePlayer;
        }
    }
}