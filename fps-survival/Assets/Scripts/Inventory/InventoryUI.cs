using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Vector3 defaultPos;
    RectTransform rectTransform;

    private void Start()
    {
        rectTransform =GetComponent<RectTransform>();
        defaultPos = rectTransform.position;
        gameObject.SetActive(false);
    }

    public void ChangeInventoryPos(Vector3 newPos)
    {
        rectTransform.position = newPos;
    }

    public void ResetPos()
    {
       rectTransform.position = defaultPos;
    }
}

