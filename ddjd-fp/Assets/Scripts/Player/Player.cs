using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    #region Camera
    [SerializeField]
    private GameObject _cinemachineCameraTarget;
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    private const float _threshold = 0.01f;
    #endregion

    #region GameObjects
    [SerializeField]
    private PlayerSettings _playerSettings;
    private CharacterController _controller;
    private GameObject _mainCamera;
    private InputHandler _playerInput;
    #endregion

    #region Runtime Attributes
    private PlayerState _currentState;
    private PlayerStateFactory _states;

    // Player
    private float _targetSpeed;
    private float _speed;
    private float _targetRotation = 0.0f;
    private float _rotationVelocity;
    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;

    // Jump
    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;
    #endregion

    #region Getters and Setters
    public CharacterController Controller  {get { return _controller; } set { _controller = value;}}
    public GameObject MainCamera  {get { return _mainCamera; } set { _mainCamera = value;}}
    public InputHandler PlayerInput {get { return _playerInput; } set { _playerInput = value;}}
    public PlayerSettings PlayerSettings {get { return _playerSettings; } set { _playerSettings = value;}}

    public PlayerState CurrentState {get { return _currentState; }  set { _currentState = value; }}

    public float TargetSpeed  {get { return _targetSpeed; }  set { _targetSpeed = value; }}
    public float Speed  {get { return _speed; }  set { _speed = value; }}
    public float TargetRotation  {get { return _targetRotation; }  set { _targetRotation = value; }}
    public float RotationVelocity {get { return _rotationVelocity; }  set { _rotationVelocity = value; }}
    public float VerticalVelocity {get { return _verticalVelocity; }  set { _verticalVelocity = value; }}
    public float TerminalVelocity  {get { return _terminalVelocity; }  set { _terminalVelocity = value; }}
    
    public float JumpTimeoutDelta {get { return _jumpTimeoutDelta; }  set { _jumpTimeoutDelta = value; }}
    public float FallTimeoutDelta  {get { return _fallTimeoutDelta; }  set { _fallTimeoutDelta = value; }}
    #endregion

    private void Awake() {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        _controller = GetComponent<CharacterController>();
        _playerInput = GetComponent<InputHandler>();

        _jumpTimeoutDelta = _playerSettings.JumpTimeout;
        _fallTimeoutDelta = _playerSettings.FallTimeout;

        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();
    }

    private void Update() {
        _currentState.UpdateStates();

        CalculateSpeed();
        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
        _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }

    private void LateUpdate() {
		CameraRotation();
	}

    #region Camera
    private void CameraRotation() {
        if (_playerInput.look.sqrMagnitude >= _threshold && !_playerSettings.LockCameraPosition) {
            _cinemachineTargetYaw += _playerInput.look.x;
            _cinemachineTargetPitch += _playerInput.look.y;
        }

        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, _playerSettings.BottomClamp, _playerSettings.TopClamp);

        _cinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + _playerSettings.CameraAngleOverride, _cinemachineTargetYaw, 0.0f);
    }

    private static float ClampAngle(float ifAngle, float ifMin, float ifMax) {
        if (ifAngle < -360f) ifAngle += 360f;
        if (ifAngle > 360f) ifAngle -= 360f;
        return Mathf.Clamp(ifAngle, ifMin, ifMax);
    }
    #endregion

    #region Player State Functions
    public bool GroundedCheck() {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - _playerSettings.GroundedOffset, transform.position.z);
        return Physics.CheckSphere(spherePosition, _playerSettings.GroundedRadius, _playerSettings.GroundLayers, QueryTriggerInteraction.Ignore);
    }

    public void CalculateSpeed() {
        float speedOffset = 0.1f;
        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;
        
        if (currentHorizontalSpeed < _targetSpeed - speedOffset || currentHorizontalSpeed > _targetSpeed + speedOffset) {
            _speed = Mathf.Lerp(currentHorizontalSpeed, _targetSpeed, Time.deltaTime * _playerSettings.SpeedChangeRate);
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else {
            _speed = _targetSpeed;
        }
    }

  public void Move() {
        Vector3 inputDirection = new Vector3(_playerInput.move.x, 0.0f, _playerInput.move.y).normalized;
        if (_playerInput.move != Vector2.zero) {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, _playerSettings.RotationSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }
    }
    #endregion 
}
