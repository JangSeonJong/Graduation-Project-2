using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    float _mouseDistance;

    bool _canSearch = false;

    int _maskEnemy = 1 << (int)Define.Layer.Enemy;
    int _maskFloor = 1 << (int)Define.Layer.Floor;

    ItemPickup _itemPickup;

    public override void Init()
    {
        _anim = GetComponent<Animator>();
        _itemPickup = GetComponent<ItemPickup>();
        _playerStatus = GetComponent<PlayerStatus>();

        InputManager.KeyAction -= KeyInputCheck;
        InputManager.KeyAction += KeyInputCheck;

        InputManager.MouseAction -= MouseInputCheck;
        InputManager.MouseAction += MouseInputCheck;
    }

    void KeyInputCheck(Define.KeyAction action)
    {
        if (!_isDead && GameManager.isGameStart)
        {
            switch (action)
            {
                case Define.KeyAction.E:
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position + transform.up * 0.3f, transform.forward, out hit))
                    {
                        if (hit.transform.tag == "ItemSpawner")
                        {
                            _itemPickup.CheckItem(hit, _canSearch);
                        }
                        if (hit.transform.tag == "Bed")
                        {
                            GameManager.instance.Sleep();
                        }
                    }
                    break;

                case Define.KeyAction.I:
                    GameManager.instance.inven.TryOpen();
                    break;

                case Define.KeyAction.M:
                    GameManager.instance.map.OpenMap();
                    break;

                case Define.KeyAction.Escape:
                    GameManager.instance.pauseMenu.TryPause();
                    break;
            }
            
        }
    }

    void MouseInputCheck(Define.MouseAction action)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!_isDead && GameManager.canMove)
        {
            switch (action)
            {
                case Define.MouseAction.LeftClick:
                    if (Physics.Raycast(ray, out hit, 100, _maskEnemy))
                    {
                        if (!_isDead)
                        {
                            _mouseDistance = (hit.point - transform.position).magnitude;
                            _destination = hit.point;
                            _lockTarget = hit.collider.gameObject;

                            _stopSkill = false;
                            _canSearch = false;

                            _enemyStatus = _lockTarget.GetComponent<Status>();

                            if (!_isAttacking && _enemyStatus.Hp > 0)
                                _state = Define.State.Moving;
                        }
                    }
                    break;
                case Define.MouseAction.LeftButtonUp:
                    {
                        _stopSkill = true;
                    }
                    break;
                case Define.MouseAction.RightClick:
                    if (Physics.Raycast(ray, out hit, 100, _maskFloor))
                    {
                        _mouseDistance = (hit.point - transform.position).magnitude;
                        _destination = hit.point;
                        _lockTarget = null;

                        _stopSkill = true;
                        _canSearch = false;

                        if (!_isAttacking)
                            _state = Define.State.Moving;
                    }
                    break;
                case Define.MouseAction.RightPress:
                    if (Physics.Raycast(ray, out hit, 100, _maskFloor))
                    {
                        _mouseDistance = (hit.point - transform.position).magnitude;
                        _destination = hit.point;

                        _canSearch = false;

                        if (!_isAttacking)
                            _state = Define.State.Moving;
                    }
                    break;
            }
        }
    }

    protected override void UpdateDie()
    {
        if (!_isDead)
            _anim.SetTrigger("doDead");

        _isDead = true;

        GameManager.instance.GameOver();
    }

    protected override void UpdateIdle()
    {
        _anim.SetBool("isWalk", false);
        _anim.SetBool("isMove", false);
        _canSearch = true;
    }

    protected override void UpdateMoving()
    {
        if (_lockTarget != null)
        {
            float distance = (_lockTarget.transform.position - transform.position).magnitude;
            if (distance <= _playerStatus.AttackRange)
            {
                _state = Define.State.Attack;

                return;
            }
        }

        Vector3 dir = _destination - transform.position;
        dir.y = 0;

        if (dir.magnitude < 0.1f)
        {
            _state = Define.State.Idle;
        }
        else
        {
            Debug.DrawRay(transform.position + transform.up * 0.3f, dir.normalized * 0.5f, Color.green);

            if (Physics.Raycast(transform.position + transform.up * 0.3f, dir, 0.5f, LayerMask.GetMask("Wall")))
            {
                if (Input.GetMouseButton(0) == false)
                    _state = Define.State.Idle;
                return;
            }

            float distance = Mathf.Clamp(_playerStatus.Speed * Time.deltaTime, 0, dir.magnitude);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 30 * Time.deltaTime);
            transform.position += dir.normalized * distance * (_mouseDistance < 1.5f ? 0.5f : 1.0f);
            _anim.SetBool("isWalk", (_mouseDistance < 1.5f));
            _anim.SetBool("isMove", true); 
            
        }
    }

    protected override void UpdateAttack()
    {
        if (_lockTarget != null)
        {
            _anim.SetBool("isWalk", false);
            _anim.SetBool("isMove", false);

            Vector3 dir = _lockTarget.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 30 * Time.deltaTime);

            if (_enemyStatus.Hp > 0)
            {
                if (_attackReady)
                {
                    _isAttacking = true;
                    _attackReady = false;
                    StartCoroutine(AttackCool());

                    _anim.SetBool("isAttack", true);
                    _anim.SetTrigger("Unarmed");

                    SoundManager.instance.Play("UnityChan/univ0001", Define.Sound.Effect);
                }
            }
        }
    }

    IEnumerator AttackCool()
    {
        yield return new WaitForSeconds(_playerStatus.AttackDelay);
        _attackReady = true;
    }

    void OnHitEvent()
    {
        if (_lockTarget != null)
        {
            _enemyStatus.SetHP(-_playerStatus.Attack);
        }
    }

    void EndAttackAnimation()
    {
        _anim.SetBool("isAttack", false);
        _isAttacking = false;

        if (_stopSkill)
        {
            _state = Define.State.Idle;
        }
        else
        {
            _state = Define.State.Attack;
        }
    }
}
