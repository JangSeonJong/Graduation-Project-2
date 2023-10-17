using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Status
{
    [SerializeField]
    protected int _maxHunger;
    [SerializeField]
    protected int _hunger;

    [SerializeField]
    protected int _maxThirst;
    [SerializeField]
    protected int _thirst;

    [SerializeField]
    float hungryTime;
    [SerializeField]
    public float MaxHungryTime;

    [SerializeField]
    float thirstyTime;
    [SerializeField]
    public float MaxThirstyTime;

    public int Hunger { get { return _hunger; } set { _hunger = value; } }
    public int MaxHunger { get { return _maxHunger; } set { _maxHunger = value; } }

    public int Thirst { get { return _thirst; } set { _thirst = value; } }
    public int MaxThirst { get { return _maxThirst; } set { _maxThirst = value; } }

    public bool isHome;

    void FixedUpdate()
    {
        if (!GameManager.isGameStart)
            return;

        Hungry();
        Thirsty();
        
    }

    public void Hungry()
    {
        if (hungryTime > 0)
        {
            hungryTime -= Time.deltaTime;

            if (hungryTime <= 0)
            {
                SetHunger(-1);

                hungryTime = MaxHungryTime;
            }
        }
    }

    public void Thirsty()
    {
        if (thirstyTime > 0)
        {
            thirstyTime -= Time.deltaTime;

            if (thirstyTime <= 0)
            {
                SetThirst(-1);

                thirstyTime = MaxThirstyTime;
            }
        }
    }

    public void SetHunger(int count)
    {
        Hunger += count;

        if (Hunger < 0)
        {
            SetHP(count);
        }

        Hunger = Mathf.Clamp(Hunger, 0, MaxHunger);
    }

    public void SetThirst(int count)
    {
        Thirst += count;

        if (Thirst < 0)
        {
            SetHP(count);
        }

        Thirst = Mathf.Clamp(Thirst, 0, MaxThirst);
    }

    public void SetStatus(int attack, float attackDelay, float attackRange)
    {
        Attack = attack;
        AttackDelay = attackDelay;
        AttackRange = attackRange;
    }

    public void ResetStatus()
    {
        Attack = 2;
        AttackDelay = 1.5f;
        AttackRange = 1.5f;
    }
}
