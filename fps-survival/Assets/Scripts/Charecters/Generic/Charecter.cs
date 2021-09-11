using UnityEngine;

public abstract class Charecter : MonoBehaviour
{
    [SerializeField]
    protected CharecterData charecterData;

    public Team team;

    public int healthCurrent
    {
        get => _healthCurrent;
        set
        {
            if (value > _maxHealth)
            {
                healthCurrent = _maxHealth;
            }
            else if (value <= 0)
            {
                healthCurrent = 0;
                Die();
            }
            else
            {
                _healthCurrent = value;
            }
        }
    }

    [SerializeField]
    protected int _maxHealth;

    [SerializeField]
    protected int _healthCurrent;
    
    [SerializeField]
    protected float _moveSpeed;

    protected virtual void Start()
    {
        SetCharecterDefaultStats();
        gameObject.tag = charecterData.team.ToString();
    }

    protected virtual void Update()
    {

    }

    protected virtual void FixedUpdate()
    {

    }

    protected virtual void Move(Vector3 direction)
    {

    }

    public virtual void Heal(int amount)
    {
        healthCurrent += amount;
    }

    public virtual void TakeDamage(int amount)
    {
        healthCurrent -= amount;
        if(healthCurrent <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log(gameObject.name + "Has died");
        Destroy(gameObject);
    }

    protected virtual void SetCharecterDefaultStats()
    {
        _maxHealth = charecterData.healthMax;
        _healthCurrent = _maxHealth;
        team = charecterData.team;
    }

    protected virtual void DropLoot()
    {

    }

    protected virtual void ChangeTeam(Team newTeam)
    {
        team = newTeam;
    }
}
