using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody _rb;
    [SerializeField] Animator _animator;

    [Header("Setting")]
    [SerializeField] float _moveSpeed = 5.0f;
    [SerializeField] AudioSource _walkAudioSource;
    [SerializeField] AudioClip _hitWallSound;
    [SerializeField] float _wallCastDistance = 1f;
    [SerializeField] Transform _playerStartPosition;

    [Header("Debug")]
    [SerializeField] Vector3 _inputMovement;
    [SerializeField] Vector3 _moveXZ;

    [SerializeField] bool _canMove;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    public void Init()
    {
        transform.position = _playerStartPosition.position;
    }

    private void Start()
    {
        CinemachineCore.GetInputAxis = GetAxisCustom;

        float GetAxisCustom(string axisName)
        {
            if(!_canMove)
            {
                return 0;
            }

            if (axisName == "Mouse X")
            {
                return UnityEngine.Input.GetAxis("Mouse X");
            }
            else if (axisName == "Mouse Y")
            {
                return UnityEngine.Input.GetAxis("Mouse Y");
            }
            return 0;
        }
    }

    public void SetCanMove(bool canMove)
    {
        _canMove = canMove;

        if(_canMove == false)
        {
            _animator.SetBool("MOVE", false);
        }
    }

    void Update()
    {
        if (!_canMove)
        {
            return;
        }

        InputKey();

        void InputKey()
        {
            _inputMovement = (Input.GetAxis("Vertical") * Camera.main.transform.forward) + (Input.GetAxis("Horizontal") * Camera.main.transform.right);
        }
    }

    void FixedUpdate()
    {
        if(!_canMove)
        {
            return;
        }

        Move();
        Rotate();

        void Move()
        {
            _moveXZ = new Vector3(_inputMovement.x, 0, _inputMovement.z); // Y축을 제외한 방향 구하기.
            _rb.MovePosition(transform.position + (_moveXZ * _moveSpeed * Time.deltaTime));

            bool isMoving = (_moveXZ.magnitude > 0) && !CheckWall();
            _animator.SetBool("MOVE", isMoving);
        }

        void Rotate()
        {
            transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        }
    }

    private bool CheckWall()
    {
        if (Physics.Raycast(this.transform.position + Vector3.up * 1.5f, _moveXZ, out RaycastHit hit, _wallCastDistance))
        {
            if (hit.transform.CompareTag("WALL"))
            {
                //print(hit.transform.name);
                return true;
            }
        }
        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("WALL"))
        {
            AudioSource.PlayClipAtPoint(_hitWallSound, collision.contacts[0].point);
        }
    }

    public void PlayWalkSound() // Bind Walk Animation Event.
    {
        _walkAudioSource.volume = _moveXZ.magnitude;
        _walkAudioSource.Play();
    }
}
