using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "Resources/New Resource")]
public class ResourceData : ScriptableObject
{
    [SerializeField]
    private string _resourceName;
    public string resourceName => _resourceName;

    [SerializeField]
    private Mesh _mesh;
    public Mesh mesh => _mesh;

    [SerializeField]
    private Resource_Type _resource_Type;
    public Resource_Type resource_Type => _resource_Type;

    [SerializeField, Range(0, 4)]
    private int _resTier;
    public int resTier => _resTier;

   // [SerializeField]
   // private Item _resouce;
   // public Item resource => _resouce;

    [SerializeField]
    private int _resAmount;
    public int resAmount => _resAmount;

    [SerializeField]
    private bool _canRespawn;
    public bool canRespawn => _canRespawn;  //Respawntime will be managed in GameManager

}
