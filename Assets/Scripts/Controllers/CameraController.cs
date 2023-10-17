using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject _player;

    [SerializeField]
    Vector3 _offset;
    [SerializeField]
    Vector3 _offsetNoraml;
    [SerializeField]
    Vector3 _offsetHome;

    [SerializeField]
    Define.CameraMode _mode = Define.CameraMode.Default;

    [SerializeField]
    float _wallTime;
    float count = 0;

    [SerializeField]
    float _zoomSpeed;
    float _height;

    void Start()
    {
        SetCamera(_mode);
        _height = _offset.magnitude;
        _player = FindObjectOfType<PlayerController>().gameObject;
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(_player.transform.position + _player.transform.up, _offset, out hit, _offset.magnitude, 1 << (int)Define.Layer.Wall))
        {
            if (count < _wallTime)
            {
                count += Time.deltaTime;
            }
            else
            {
                _height = Mathf.Lerp(_height, ((hit.point - _player.transform.position + _player.transform.up * 0.5f).magnitude * 0.6f), Time.deltaTime * _zoomSpeed);
            }
        }
        else
        {
            _height = Mathf.Lerp(_height, _offset.magnitude, Time.deltaTime * _zoomSpeed);
            count = 0;
        } 
    }

    void LateUpdate()
    {
        if (_player == null)
            return;

        transform.position = _player.transform.position + _player.transform.up * 0.5f + _offset.normalized * _height;
        transform.LookAt(_player.transform.position + _player.transform.up * 0.5f);

    }

    public void SetCamera(Define.CameraMode mode)
    {
        _mode = mode;

        switch (_mode)
        {
            case Define.CameraMode.Default:
                _offset = _offsetNoraml;
                break;
            case Define.CameraMode.House:
                _offset = _offsetHome;
                break;
        }
    }
}
