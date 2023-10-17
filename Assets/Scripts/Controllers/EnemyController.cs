using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : BaseController
{
    [SerializeField]
    float _scanRange;

    [SerializeField]
    GameObject player;

    bool _isSelected = false;
    bool isPlaying = false;

    Outline _outline;
    FieldOfViewAngle _fov;
    NavMeshAgent _nma;
    AudioSource audioSource;

    public override void Init()
    {
        _outline = GetComponent<Outline>();
        _anim = GetComponent<Animator>();
        _fov = GetComponent<FieldOfViewAngle>();
        _nma = GetComponent<NavMeshAgent>();
        _enemyStatus = GetComponent<Status>();
        audioSource = GetComponent<AudioSource>();

        if (player == null)
            player = FindObjectOfType<PlayerController>().gameObject;
    }

    protected override void UpdateDie()
    {
        if (!_isDead)
        {
            _isDead = true;
            _stopSkill = true;

            _anim.SetTrigger("doDead");
            _anim.SetBool("isDead", _isDead);
        }
        //else
        //{
        //    if (_enemyStatus.Hp > 0)
        //    {
        //        _isDead = false;
        //        _anim.SetBool("isDead", _isDead);

        //        _state = Define.State.Idle;
        //    }
        //}
    }

    protected override void UpdateIdle()
    {
        if (!_isDead && GameManager.isGameStart)
        {
            _anim.SetBool("isWalk", false);
            _anim.SetBool("isRun", false);

            if (player == null)
                return;


            float distance = (player.transform.position - transform.position).magnitude;

            if (distance <= _scanRange || _fov.SearchTarget())
            {
                _lockTarget = player;
                _playerStatus = _lockTarget.GetComponent<PlayerStatus>();

                if (_playerStatus.Hp <= 0)
                    return;

                _state = Define.State.Moving;

                return;
            }
        }
    }

    protected override void UpdateMoving()
    {
        if (!_isAttacking && !_isDead)
        {
            if (_lockTarget != null)
            {
                _destination = _lockTarget.transform.position;
                float distance = (_lockTarget.transform.position - transform.position).magnitude;
                if (distance <= _enemyStatus.AttackRange)
                {
                    _nma.SetDestination(transform.position);

                    _state = Define.State.Attack;
                    return;
                }
            }

            Vector3 dir = _destination - transform.position;
            dir.y = 0;

            if (dir.magnitude < 0.01f)
            {
                _state = Define.State.Idle;
            }
            else
            {
                _nma.SetDestination(_destination);
                _nma.speed = _enemyStatus.Speed;

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 30 * Time.deltaTime);

                _anim.SetBool("isRun", true);
                if (!isPlaying)
                    StartCoroutine(PlayDelay("Zombie/Damaged"));
            }
        }
        
    }

    protected override void UpdateAttack()
    {
        if (_lockTarget != null || !_isDead)
        {
            _anim.SetBool("isRun", false);
            _anim.SetBool("isWalk", false);

            Vector3 dir = _lockTarget.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 30 * Time.deltaTime);

            float distance = (_lockTarget.transform.position - transform.position).magnitude;
            if (distance > _enemyStatus.AttackRange)
            {
                _state = Define.State.Moving;

                return;
            }

            if (_attackReady)
            {
                _isAttacking = true;
                _attackReady = false;
                StartCoroutine(AttackCool());

                _anim.SetTrigger("doAttack");
            }
        }
    }

    IEnumerator AttackCool()
    {
        yield return new WaitForSeconds(_enemyStatus.AttackDelay);
        _attackReady = true;
    }

    void OnHitEvent()
    {
        if (_lockTarget != null)
        {
            _playerStatus.SetHP(-_enemyStatus.Attack);
        }

        Play("Zombie/Attacking");
    }

    void EndAttackAnimation()
    {
        _isAttacking = false;


        _state = Define.State.Idle;
    }

    protected void Outline()
    {
        if (!_isDead)
            _outline.enabled = _isSelected;
    }

    protected void OnMouseEnter()
    {
        _isSelected = true;
        Outline();
    }

    protected void OnMouseOver()
    {
        if (_isDead)
            _outline.enabled = false;
    }

    protected void OnMouseExit()
    {
        _isSelected = false;
        Outline();
    }

    void Play(string path, Define.Sound type = Define.Sound.Effect)
    {
        AudioClip audioClip = SoundManager.instance.GetAudioClip(path, type);

        if (audioClip != null)
        {
            if(!audioSource.isPlaying)
                audioSource.PlayOneShot(audioClip);
        }
    }

    IEnumerator PlayDelay(string path, Define.Sound type = Define.Sound.Effect)
    {
        isPlaying = true;

        Play(path, type);

        yield return new WaitForSeconds(Random.Range(3, 6));

        isPlaying = false;
    }
}
