using PlayerInput;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveForce = 5;
    [SerializeField] private float _jumpForce = 5;

    private Rigidbody _rigidbody;
    private PlayerDefaultInput _playerInput;
    private Vector2 _moveInputValue;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        // Action�X�N���v�g�̃C���X�^���X����
        _playerInput = new PlayerDefaultInput();

        // Action�C�x���g�o�^
        _playerInput.Default.Move.started += OnMove;
        _playerInput.Default.Move.performed += OnMove;
        _playerInput.Default.Move.canceled += OnMove;
        _playerInput.Default.Jump.performed += OnJump;

        // Input Action���@�\�����邽�߂ɂ́A
        // �L��������K�v������
        _playerInput.Enable();
    }

    private void OnDestroy()
    {
        // ���g�ŃC���X�^���X������Action�N���X��IDisposable���������Ă���̂ŕK��Dispose����K�v������
        _playerInput?.Dispose();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        // Move�A�N�V�����̓��͎擾
        _moveInputValue = context.ReadValue<Vector2>();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        // �W�����v����͂�^����
        _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        // �ړ������̗͂�^����
        _rigidbody.AddForce(new Vector3(
            _moveInputValue.x,
            0,
            _moveInputValue.y
        ) * _moveForce);
    }
}
