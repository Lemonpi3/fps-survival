using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class Proyectile : NetworkBehaviour
{
    Rigidbody rb;

    int _damage;
    float _range;

    int _resDmg;
    int _resTier;

    float speed;
    float lifeCounter;
    Vector3 spawnPos;

    Collider _collider;

    Team _team;
    Resource_Type _resType;

    bool impacted;
    // Update is called once per frame
    void Start()
    {
        lifeCounter = GameManager.instance.stoppedProyectileLife;
        Debug.Log("ProyectileSpawned");
        _collider = GetComponent<Collider>(); 
    }

    void Update()
    {
        speed = rb.velocity.magnitude;

        if(speed < 0.5)
        {
            lifeCounter -= Time.deltaTime;
            if(lifeCounter <= 0){
                Debug.Log("ProyectiledeSpawning");
                Destroy(gameObject);
            }
        }
        
    }

    public void InitializeProyectile(int charDamage , float range, Team team,Transform camTransform ,float bulletSpeed,int resDmg=0,int resTier=0,Resource_Type resType = Resource_Type.None)
    {
        _damage = charDamage;
        _range = range;
        _team = team;

        _resDmg = resDmg;
        _resTier = resTier;
        _resType = resType;

        rb = GetComponent<Rigidbody>();
        transform.rotation = camTransform.rotation;
        rb.AddForce(camTransform.forward * bulletSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != this.tag)
        {
            Debug.Log("ProyectiledeInpacted");
            rb.constraints = RigidbodyConstraints.FreezeAll;
            OnInpact(other);
            _collider.enabled = false;
        }
    }

    private void OnInpact(Collider other)
    {
        Charecter _target = other.GetComponent<Charecter>();
        if (other.tag != "Resource" && _target != null && _target.team != _team)
        {
            Debug.Log(_damage);
            _target.TakeDamage(_damage);
        }else
        if (other.tag == "Resource")
        {
            Debug.Log(other.GetComponent<ResourceNode>().GatherResource(_resType, _resTier, _resDmg));
        }
    }
}
