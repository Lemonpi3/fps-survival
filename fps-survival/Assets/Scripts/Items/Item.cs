using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField]
    private Sprite _icon;
    public Sprite icon => _icon;

    [SerializeField]
    private string _itemName;
    public string itemName => _itemName;

    [SerializeField]
    private string _description;
    public string description => _description;

    [SerializeField]
    private int _maxStackSize;
    public int maxStackSize => _maxStackSize;

    public virtual void OnUseItem()
    {
        Debug.Log("Used " + itemName);
    }

}
