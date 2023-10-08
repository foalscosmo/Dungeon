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
   [SerializeField] private CharacterAnimator characterAnimator;
   private float _animationBlend;
   private const float SpeedChangeRate = 0.6f;
   private static readonly int Speed = Animator.StringToHash("Speed");


   [Header("Character Move Variables")]
   [SerializeField] private float currentSpeed;
   [SerializeField] private float moveSpeed;
   public float BaseMoveSpeed { get; private set; }
   public float MoveSpeed { set => moveSpeed = value; }
   public float CurrentSpeed => currentSpeed;
   private Vector3 _moveVector;


   [Header("Character Rotation Variables")]
   private float _rotationVelocity;

   private float _targetRotation;
   private const float SmoothRotation = 0.06f;

   private void Start()
   {
      if (Camera.main != null) mainCamera = Camera.main.transform;
      BaseMoveSpeed = moveSpeed;
   }

   private void Update()
   {
      CheckSpeed();
      SetAnimation();
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
      currentSpeed = _moveVector == Vector3.zero ? 0 : moveSpeed;
   }

   private void SetAnimation()
   {
      _animationBlend = Mathf.Lerp(_animationBlend, currentSpeed, SpeedChangeRate);
      characterAnimator.Animator.SetFloat(Speed,_animationBlend);
   }

}