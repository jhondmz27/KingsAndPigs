using UnityEngine;
using UnityEngine.InputSystem;
public class GatterInputs : MonoBehaviour
{
    private Controls controls;
    [SerializeField] private float _valueX;
    public float ValueX { get => _valueX; }

    [SerializeField] private bool _isJumping;
    public bool IsJumping { get => _isJumping; set => _isJumping = value; }

    private void Awake()
    {
        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.Player.Move.performed += StartMove;
        controls.Player.Move.canceled += StopMove;
        controls.Player.Jump.performed += StartJump;
        controls.Player.Jump.canceled += StopJump;
        controls.Player.Enable();
    }

    private void StartMove(InputAction.CallbackContext context)
    {
        _valueX = Mathf.RoundToInt(context.ReadValue<float>());
    }

    private void StopMove(InputAction.CallbackContext context)
    {
        _valueX =  0;
    }

    private void StartJump(InputAction.CallbackContext context)
    {
        _isJumping = true;
    }

    private void StopJump(InputAction.CallbackContext context)
    {
        _isJumping = false;
    }

    private void OnDisable()
    {
        controls.Player.Move.performed -= StartMove;
        controls.Player.Move.canceled -= StopMove;
        controls.Player.Jump.performed -= StartJump;
        controls.Player.Jump.canceled -= StopJump;
        controls.Player.Disable();
    }
}
