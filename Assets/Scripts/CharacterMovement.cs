using UnityEngine;

public class CharacterMovement : MonoBehaviour,IMovable
{
   [Header("Camera Variables")]
   [SerializeField] private Transform mainCamera;
   
   [Header("Character Physics Variables")]
   [SerializeField] private CharacterController characterController;

   [Header("Character Inputs")]
   [SerializeField] private CharacterInputs characterInputs;

   [Header("Character Animation Variables")]
   [SerializeField] Animator animator;
   private float _animationBlend;
   private const float SpeedChangeRate = 10;
   private static readonly int Speed = Animator.StringToHash("Speed");
   private static readonly int MotionSpeed = Animator.StringToHash("MotionSpeed");

   [Header("Character Move Variables")]
   [SerializeField] private float currentSpeed;
   [SerializeField] private float walkSpeed;
   [SerializeField] private float dashSpeed;
   [SerializeField] private float dashDuration;
   [SerializeField] private float dashCoolDown;
   private Vector3 _moveVector;
   private bool _isDashing;
   private float _dashTimer, _cooldownTimer;

   
   [Header("Character Rotation Variables")]
   private float _rotationVelocity;
   private float _targetRotation;
   private const float SmoothRotation = 0.1f;

   private void Start()
   {
      if (Camera.main != null) mainCamera = Camera.main.transform;
   }

   private void Update()
   {
      CheckSpeed();
      SetAnimation();
      Dash();
   }

   private void FixedUpdate()
   {
      BasicMovement();
   }

   public void BasicMovement()
   {
      _moveVector = new Vector3(characterInputs.Horizontal, 0.0f, characterInputs.Vertical).normalized;
      if (_moveVector == Vector3.zero) return;
      _targetRotation = Mathf.Atan2(characterInputs.Horizontal, characterInputs.Vertical) * Mathf.Rad2Deg +
                        mainCamera.transform.localEulerAngles.y;
      var rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
         SmoothRotation);
      transform.rotation = Quaternion.Euler(0.0f,rotation,0.0f);
      var targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
      characterController.Move(targetDirection.normalized * (currentSpeed * Time.deltaTime));
   }

   private void CheckSpeed()
   {
      currentSpeed = _moveVector != Vector3.zero ? _isDashing ? dashSpeed : walkSpeed : 0;
   }

   private void SetAnimation()
   {
      _animationBlend = Mathf.Lerp(_animationBlend, currentSpeed, Time.deltaTime * SpeedChangeRate);
      animator.SetFloat(Speed,_animationBlend);
      animator.SetFloat(MotionSpeed,_moveVector.magnitude);
   }

   public void Dash()
   {
      switch (_isDashing)
      {
         case false when _cooldownTimer <= 0.0f:
         {
            if (characterInputs.IsDashing) StartDash();
            break;
         }
         case false:
            _cooldownTimer -= Time.deltaTime;
            break;
         default:
         {
            _dashTimer -= Time.deltaTime;
            if (_dashTimer <= 0.0f) EndDash();
            break;
         }
      }
   }
   
   private void StartDash()
   {
      _isDashing = true;
      _dashTimer = dashDuration;
   }

   private void EndDash()
   {
      _isDashing = false;
      _cooldownTimer = dashCoolDown;
   }
}