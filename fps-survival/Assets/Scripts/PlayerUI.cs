using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] BuilderUI _builderUI;
    public BuilderUI builderUI => _builderUI;

    [SerializeField] Transform _crossChair;
    public Transform crossChair => _crossChair;

    [SerializeField] GameObject menuUI;

    public void DestroyNonLocalUI()
    {
        Destroy(menuUI);
    }
}
