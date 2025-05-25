using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class SquadronMovement : MonoBehaviour
{

    [SerializeField]
    private float speed;
    [SerializeField]
    private float rotateSpeed;

    private Rigidbody2D _rigidBody;
    private PlayerInput _playerInput;
    private Vector2 _inputVector;
    public void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _playerInput.onActionTriggered += OnTriggerAction;
    }

    private void OnTriggerAction(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
        {
            if (context.action.name == "Move")
            {
                _inputVector = context.ReadValue<Vector2>();
              
            }
        }
    }

    public void Update()
    {
        Move(_inputVector);
        Rotate(); //임시 추후 내부의 우주선 Rotate
    }

    private void Rotate()
    {
        if (_inputVector != Vector2.zero)
        {
            float angle = Mathf.Atan2(_inputVector.y, _inputVector.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward); // -90도는 기본적으로 up 방향을 기준으로 할 경우 보정
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        }
    }

    private void Move(Vector2 v)
    {
        _rigidBody.AddForce(v*speed);
    }
}
