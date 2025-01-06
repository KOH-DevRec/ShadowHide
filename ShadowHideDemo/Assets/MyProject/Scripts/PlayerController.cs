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

        // Actionスクリプトのインスタンス生成
        _playerInput = new PlayerDefaultInput();

        // Actionイベント登録
        _playerInput.Default.Move.started += OnMove;
        _playerInput.Default.Move.performed += OnMove;
        _playerInput.Default.Move.canceled += OnMove;
        _playerInput.Default.Jump.performed += OnJump;

        // Input Actionを機能させるためには、
        // 有効化する必要がある
        _playerInput.Enable();
    }

    private void OnDestroy()
    {
        // 自身でインスタンス化したActionクラスはIDisposableを実装しているので必ずDisposeする必要がある
        _playerInput?.Dispose();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        // Moveアクションの入力取得
        _moveInputValue = context.ReadValue<Vector2>();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        // ジャンプする力を与える
        _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        // 移動方向の力を与える
        _rigidbody.AddForce(new Vector3(
            _moveInputValue.x,
            0,
            _moveInputValue.y
        ) * _moveForce);
    }
}
