using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Charecter
{
    [SerializeField]
    private int currentWeapon = 0;
    [SerializeField]
    private Weapon[] weapons;

    [SerializeField]
    GroundCheck groundCheck;
    [SerializeField]
    float jumpForce;

    Vector3 direction;
    Rigidbody rb;
    

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        LoadWeapons();
    }

    protected override void Update()
    {
        base.Update();
        GetInputs();
    }

    protected override void FixedUpdate()
    {
        direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
    }

    protected override void SetCharecterDefaultStats()
    {
        base.SetCharecterDefaultStats();
    }

    protected override void Die()
    {
        base.Die();
    }

    protected override void Heal(int amount)
    {
        base.Heal(amount);
    }

    protected override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
    }

    protected override void Move(Vector3 direction)
    {
        rb.velocity = new Vector3(direction.x * _moveSpeed,rb.velocity.y,direction.z * _moveSpeed);
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
    }

    private void GetInputs()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            Move(direction);
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if(Input.mouseScrollDelta.y > 0)
        {
            SwapWeapons(currentWeapon + 1);
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            SwapWeapons(currentWeapon - 1);
        }
    }

    private void SwapWeapons(int newSlot)
    {
        weapons[currentWeapon].gameObject.SetActive(false);

        if(newSlot >= weapons.Length)
        {
            currentWeapon = 0;
        }
        else if(newSlot < 0)
        {
            currentWeapon = weapons.Length - 1;
        }
        else
        {
            currentWeapon = newSlot;
        }
        Debug.Log(currentWeapon);
        weapons[currentWeapon].gameObject.SetActive(true);
    }

    private void LoadWeapons()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(false);
        }

        SwapWeapons(currentWeapon);
    }
}
