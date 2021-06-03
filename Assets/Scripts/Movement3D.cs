using UnityEngine;

public class Movement3D : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f; // 이동 속도
    [SerializeField] private float gravity = -9.81f; // 중력 계수
    [SerializeField] private float jumpForce = 3.0f; // 뛰어 오르는 힘
    private Vector3 _moveDirection, _distance, _moveDis; // 이동 방향

    [SerializeField] private Transform cameraTransform;
    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // 플레이어가 땅을 밟고 있지 않다면
        // y축 이동방향에 gravity * Time.deltaTime을 더해준다
        if (_characterController.isGrounded == false)
        {
            _moveDirection.y += gravity * Time.deltaTime;
        }

        // 카메라가 바라보고 있는 방향을 기준으로 방향키에 따라 이동할 수 있도록 함
        _moveDis = cameraTransform.rotation * _distance;
        _moveDirection = new Vector3(_moveDis.x, _moveDirection.y, _moveDis.z);

        _characterController.Move(_moveDirection * (moveSpeed * Time.deltaTime));
    }

    public void MoveTo(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        _distance = new Vector3(ctx.ReadValue<Vector2>().x, 0, ctx.ReadValue<Vector2>().y);
    }

    public void JumpTo(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        if (ctx.performed && _characterController.isGrounded)
        {
            _moveDirection.y = jumpForce;
        }
    }
}