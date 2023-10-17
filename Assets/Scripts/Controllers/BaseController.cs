using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    public Define.State _state = Define.State.Idle;

    [SerializeField]
    protected GameObject _lockTarget;
    protected Animator _anim;
    protected Vector3 _destination;
    protected Status _enemyStatus;
    protected PlayerStatus _playerStatus;

    protected bool _stopSkill = false;
    public bool _isDead = false;

    [SerializeField]
    protected bool _attackReady = true;
    protected bool _isAttacking = false;

    void Start()
    {
        Init();
    }

    void Update()
    {
        switch (_state)
        {
            case Define.State.Die:
                UpdateDie();
                break;
            case Define.State.Idle:
                UpdateIdle();
                break;
            case Define.State.Moving:
                UpdateMoving();
                break;
            case Define.State.Attack:
                UpdateAttack();
                break;
        }
    }

    public abstract void Init();

    protected virtual void UpdateDie() { }
    protected virtual void UpdateMoving() { }
    protected virtual void UpdateIdle() { }
    protected virtual void UpdateAttack() { }
}
