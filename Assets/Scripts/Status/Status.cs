using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    [SerializeField]
    protected int _hp;
    [SerializeField]
    protected int _maxHp;
    [SerializeField]
    protected int _attack;
    [SerializeField]
    protected float _attackDelay;
    [SerializeField]
    protected float _attackRange;
    [SerializeField]
    protected float _speed;

    public int Hp { get { return _hp; } set { _hp = value; } }
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public int Attack { get { return _attack; } set { _attack = value; } }
    public float AttackDelay { get { return _attackDelay; } set { _attackDelay = value; } }
    public float AttackRange { get { return _attackRange; } set { _attackRange = value; } }
    public float Speed { get { return _speed; } set { _speed = value; } }

    public void SetHP(int count)
    {
        Hp += count;

        if (Hp <= 0)
        {
            BaseController controller = GetComponent<BaseController>();
            if (controller != null)
            {
                controller._state = Define.State.Die;
            }
        }

        Hp = Mathf.Clamp(Hp, 0, MaxHp);
    }
}
