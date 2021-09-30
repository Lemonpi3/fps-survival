using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToSurface : MonoBehaviour
{
    [SerializeField] LayerMask layerOfGameObjectToSnap;
    // Start is called before the first frame update
    void Awake()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, layerOfGameObjectToSnap))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }
    }
}
