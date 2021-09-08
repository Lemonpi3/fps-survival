using MLAPI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    private void Start()
    {
        if (!IsLocalPlayer)
        {
            GetComponent<RigidbodyFirstPersonController>().enabled = false;
            Destroy(transform.Find("Camera").gameObject);
        }
    }

    /// <summary>
    /// Gets player screen to point ray hit, returns Raycasthit.
    /// </summary>
    public Collider GetRaycastHit()
    {
        Camera cam = GetComponentInChildren<Camera>();
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider;
        }
        else return null;
    }
}
    

