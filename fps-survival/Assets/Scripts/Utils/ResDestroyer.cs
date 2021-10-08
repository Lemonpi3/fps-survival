using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] LayerMask destroyFromLayer;
    [SerializeField] string destroyFromTag = "PH tag";

    [SerializeField] bool removeDestroyerAfterATime;
    [SerializeField] float destroyerLifetime = 5f;

    private void Start()
    {
        if (removeDestroyerAfterATime)
        {
            Destroy(gameObject, destroyerLifetime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == destroyFromTag || other.gameObject.layer == destroyFromLayer)
        {
            Destroy(other.gameObject);
        }
    }
}
