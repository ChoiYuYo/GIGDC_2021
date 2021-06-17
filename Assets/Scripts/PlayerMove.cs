using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public Transform cam;
    public Rigidbody rigid;

    public float speed = 6;

    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    private Vector3 moveDir;

    private bool _isGrounded;
    private Vector2 _moveInput;
    private bool _isMove;

    private void FixedUpdate()
    {
        _isMove = _moveInput.magnitude != 0;
        
        var direction = new Vector3(_moveInput.x, 0f, _moveInput.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }
        
        if (_isMove)
        {
            rigid.velocity = new Vector3(moveDir.x*speed, rigid.velocity.y, moveDir.z*speed);
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (!ctx.started) return;
        if (!_isGrounded) return;
        _isGrounded = false;
        rigid.AddForce(Vector3.up * 45, ForceMode.Impulse);
        Debug.Log("점프");
    }
    
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }
}