using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item",menuName = "Items/Test/New Item")]
public class Item : ScriptableObject
{
    [SerializeField]
    private int _stackSize;
    public int stackSize => _stackSize;

    [SerializeField]
    private Sprite _icon;
    public Sprite icon => _icon;
}
