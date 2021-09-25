using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected bool interacted;
    protected bool isPlayer;
    protected Charecter interactor;
    [SerializeField] float interactRange = 3;

    public virtual void Interact(Charecter charecter)
    {
        if (interacted) { return; }

        interacted = true;
        interactor = charecter;
        Debug.Log(interactor + " interacted with " + gameObject);
        if(interactor.tag == "Team1"|| interactor.tag == "Team2")
        {
            isPlayer = true;
            interactor = charecter as Player;
        }
    }

    protected virtual void Update()
    {
        if ((interacted && isPlayer))
        {
            if(Input.GetKeyDown(KeyCode.Escape) || Vector3.Distance(transform.position, interactor.transform.position) > interactRange)
            {
                StopInteracting();
            }
        }
    }

    public virtual void StopInteracting()
    {
        interacted = false;
        interactor = null;
        isPlayer = false;
    }
}
