using UnityEngine.Events;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum Direction
    {
        LEFT,
        RIGHT,
    }

    #region EVENTS
    [HideInInspector] public UnityEvent GroundedEvent = new UnityEvent();
    [HideInInspector] public UnityEvent NotGroundedEvent = new UnityEvent();
    [HideInInspector] public UnityEvent JumpEvent = new UnityEvent();
    [HideInInspector] public UnityEvent BonusJumpEvent = new UnityEvent();
    [HideInInspector] public UnityEvent DashEvent = new UnityEvent();
    [HideInInspector] public UnityEvent DashEndEvent = new UnityEvent();
    #endregion

    #region GENERAL
    private Rigidbody2D m_rigidbody;
    private Collider2D m_collider;
    private Animator m_animator;
    private PlayerBase m_playerBase;
    private Direction m_facingDirection;
    private Vector2 m_newVelocity;
    private bool m_isGrounded;
    private bool m_isPushing;
    private float m_groundedDuration;
    private float m_distanceToGround;
    #endregion

    #region MOVEMENT
    [Header("MOVEMENT SETTINGS")]
    [SerializeField] private string m_horizontalInput;
    [SerializeField] private float m_horizontalDeathZone;
    [SerializeField] private float m_minimumVelocityToFlip;
    [SerializeField] private float m_minimumVelocityToTriggerAnimation;
    [SerializeField] private float m_maxSpeed;
    [SerializeField] private string m_verticalInput;
    [SerializeField] private float m_verticalDeathZone;
    [SerializeField] private float m_durationToGoDownInSeconds;
    [SerializeField] private float m_groundedConsistencyDelay;
    [SerializeField] private float m_isPushingDetectionDistance;
    private float m_goDownTimer;
    #endregion

    #region JUMP
    [Header("JUMP SETTINGS")]
    [SerializeField] private string m_jumpInput;
    [SerializeField] private float m_jumpStrength;
    [SerializeField] private uint m_bonusJumps;
    [SerializeField] private GroundedTrigger m_groundedTrigger;
    private uint m_bonusJumpsCounter;
    #endregion

    #region DASH
    [Header("DASH SETTINGS")]
    [SerializeField] private string m_dashInput;
    [SerializeField] private float m_dashMinimumVelocity;
    [SerializeField] private float m_dashMaximalVelocity;
    [SerializeField] private float m_dashMaxDuration;
    [SerializeField] private float m_dashLimit;
    [SerializeField] private LayerMask m_dashStopWhenCollideWith;
    private Vector2 m_dashVelocity;
    private Vector2 m_dashDirection;
    private float m_dashTimer;
    private bool m_isDashing;
    #endregion

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<Collider2D>();
        m_animator = GetComponent<Animator>();
        m_playerBase = GetComponent<PlayerBase>();
        m_groundedTrigger.GroundedEvent.AddListener(OnGrounded);
        m_groundedTrigger.NotGroundedEvent.AddListener(OnNotGrounded);
    }

    private void Start()
    {
        m_facingDirection = Direction.LEFT;
        m_bonusJumpsCounter = 0;
        m_isDashing = false;
        m_distanceToGround = m_collider.bounds.extents.y;
        ResetGoDownTimer();
    }

    private void Update()
    {
        HandleInputs();
        CheckFacing();
        CheckIfPushing();
        UpdateAnimations();
        UpdateTimers();
    }

    private void UpdateTimers()
    {
        if (m_isGrounded)
            m_groundedDuration += Time.deltaTime;
    }

    private void CheckIfPushing()
    {
        m_isPushing = false;
        Vector3 front = m_facingDirection == Direction.LEFT ? Vector3.left : Vector3.right;

        var colliderHalfWidth = m_collider.bounds.extents.x;
        var colliderHalfHeight = m_collider.bounds.extents.y;
        var left = transform.position + front * (colliderHalfWidth + m_isPushingDetectionDistance) + Vector3.down * colliderHalfHeight;
        var right = transform.position + front * colliderHalfWidth + Vector3.up * colliderHalfHeight;
        var hits = Physics2D.OverlapAreaAll(left, right);

        foreach (var hit in hits)
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Movable"))
                m_isPushing = true;
        }
    }

    private void HandleInputs()
    {
        InitNewVelocity();
        UpdateHorizontalVelocity();
        HandleGoDownInput();
        HandleJumpInput();
        HandleDashInput();
        UpdateDashVelocity();
        ApplyNewVelocity();
    }

    private void ResetGoDownTimer()
    {
        m_goDownTimer = m_durationToGoDownInSeconds;
    }

    private void InitNewVelocity()
    {
        m_newVelocity = new Vector2(0.0f, m_rigidbody.velocity.y);
    }

    private void UpdateDashVelocity()
    {
        if (m_isDashing)
        {
            m_dashTimer += Time.deltaTime;

            m_dashVelocity = m_dashDirection * Mathf.Lerp(m_dashMaximalVelocity, m_dashMinimumVelocity, m_dashTimer / m_dashMaxDuration);
        }

        if (m_dashTimer >= m_dashMaxDuration)
            StopDashing();
    }

    private void ApplyNewVelocity()
    {
        m_rigidbody.velocity = m_newVelocity + m_dashVelocity;
    }

    private void UpdateAnimations()
    {
        bool isWalking = Mathf.Abs(m_rigidbody.velocity.x) > m_minimumVelocityToTriggerAnimation && m_isGrounded;

        m_animator.SetBool("IsGrounded", IsGroundedWithConsistency());
        m_animator.SetBool("IsDashing", m_isDashing);
        m_animator.SetBool("IsPushing", m_isPushing && isWalking);
        m_animator.SetBool("IsWalking", isWalking);
        m_animator.SetFloat("HorizontalVelocity", m_rigidbody.velocity.x);
        m_animator.SetFloat("VerticalVelocity", m_rigidbody.velocity.y);
        m_animator.SetFloat("MovementSpeedRatio", Mathf.Abs(m_rigidbody.velocity.x) / m_maxSpeed);
    }

    private void UpdateHorizontalVelocity()
    {
        var slopeCoefficient = 1.0f;

        float horizontalAxis = Input.GetAxisRaw(m_horizontalInput);
        if (Mathf.Abs(horizontalAxis) >= m_horizontalDeathZone)
            m_newVelocity.x = horizontalAxis * Mathf.Lerp(Mathf.Abs(m_newVelocity.x), m_maxSpeed, 1.0f) * slopeCoefficient;
    }

    private void HandleGoDownInput()
    {
        OneWayCollisionPlatform oneWayCollisionPlatform;

        if (Input.GetAxisRaw(m_verticalInput) <= -m_verticalDeathZone && CanGetDown(out oneWayCollisionPlatform))
        {
            m_goDownTimer -= Time.deltaTime;
            if (m_goDownTimer <= 0.0f)
            {
                oneWayCollisionPlatform.Ignore(m_collider);
                ResetGoDownTimer();
            }
        }
        else
        {
            ResetGoDownTimer();
        }
    }

    private bool CanGetDown(out OneWayCollisionPlatform oneWayCollisionPlatform)
    {
        var colliderHalfWidth = m_collider.bounds.extents.x;
        var left = transform.position - new Vector3(0, m_distanceToGround, 0) - new Vector3(colliderHalfWidth, 0.0f, 0.0f);
        var right = transform.position - new Vector3(0, m_distanceToGround, 0) + new Vector3(colliderHalfWidth, 0.0f, 0.0f);
        var hits = Physics2D.OverlapAreaAll(left, right - new Vector3(0.0f, 0.1f, 0.0f));
        
        foreach (var hit in hits)
        {
            oneWayCollisionPlatform = hit.transform.GetComponent<OneWayCollisionPlatform>();
            if (oneWayCollisionPlatform != null)
                return true;
        }

        oneWayCollisionPlatform = null;
        return false;
    }

    private void HandleJumpInput()
    {
        if (Input.GetButtonDown(m_jumpInput))
        {
            if (m_isGrounded)
                Jump(false);
            else if (m_playerBase.CanUseAction() && m_bonusJumpsCounter < m_bonusJumps)
                Jump(true);
        }
    }

    private void HandleDashInput()
    {
        if (Input.GetButtonDown(m_dashInput) && !m_isGrounded && m_playerBase.CanUseAction())
            Dash();
    }

    private void Jump(bool p_isBonus)
    {
        if (p_isBonus)
        {
            ++m_bonusJumpsCounter;
            BonusJumpEvent.Invoke();
        }
        else
        {
            JumpEvent.Invoke();
        }

        m_animator.SetTrigger("OnJump");
        m_newVelocity.y = m_jumpStrength;
    }

    private void Dash()
    {
        m_dashDirection = DirectionToVector(m_facingDirection);
        m_dashTimer = 0.0f;
        m_isDashing = true;
        DashEvent.Invoke();
    }

    private void StopDashing()
    {
        m_dashVelocity = Vector2.zero;
        m_isDashing = false;
        DashEndEvent.Invoke();
    }

    private Vector2 DirectionToVector(Direction p_direction)
    {
        switch (p_direction)
        {
            case Direction.LEFT:
                return Vector2.left;

            case Direction.RIGHT:
                return Vector2.right;
        }

        return Vector2.zero;
    }

    private void CheckFacing()
    {
        if (m_rigidbody.velocity.x < -m_minimumVelocityToFlip && m_facingDirection == Direction.RIGHT || m_rigidbody.velocity.x > m_minimumVelocityToFlip && m_facingDirection == Direction.LEFT)
        {
            if (m_facingDirection == Direction.LEFT)
                m_facingDirection = Direction.RIGHT;
            else if (m_facingDirection == Direction.RIGHT)
                m_facingDirection = Direction.LEFT;

            FlipPlayer();
        }
    }

    private void FlipPlayer()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnGrounded()
    {
        if (!m_isGrounded)
        {
            m_isGrounded = true;
            m_bonusJumpsCounter = 0;
            GroundedEvent.Invoke();
        }
    }

    private void OnNotGrounded()
    {
        if (m_isGrounded)
        {
            m_isGrounded = false;
            m_groundedDuration = 0.0f;
            NotGroundedEvent.Invoke();
        }
    }

    public bool IsGrounded()
    {
        return m_isGrounded;
    }

    public bool IsGroundedWithConsistency()
    {
        return m_rigidbody.velocity.y <= 0 || (m_isGrounded && m_groundedDuration >= m_groundedConsistencyDelay);
    }

    public float GetGroundedDuration()
    {
        return m_groundedDuration;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (LayerTools.IsInLayerMask(m_dashStopWhenCollideWith, collision.gameObject.layer))
            StopDashing();
    }
}