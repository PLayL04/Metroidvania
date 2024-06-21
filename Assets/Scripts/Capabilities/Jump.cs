using UnityEngine;

[RequireComponent(typeof(Controller))]
public class Jump : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)] private float _jumpHeight = 3f;
    [SerializeField, Range(0, 5)] private int _maxAirJumps = 0;
    [SerializeField, Range(0f, 5f)] private float _downwardMovementMultiplier = 3f;
    [SerializeField, Range(0f, 5f)] private float _upwardMovementMultiplier = 1.7f;
    [SerializeField, Range(0f, 0.3f)] private float _coyoteTime = 0.2f;
    [SerializeField, Range(0f, 0.3f)] private float _jumpBufferTime = 0.2f;

    private Controller _controller;
    private Rigidbody2D _body;
    private CollisionRetriever _collisionRetriever;
    private Vector2 _velocity;

    private int _jumpPhase;
    private float _defaultGravityScale, _jumpSpeed, _coyoteCounter, _jumpBufferCounter;

    private bool _desiredJump, _onGround, _isJumping, _isJumpReset, _onWall;


    // Start is called before the first frame update
    void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _collisionRetriever = GetComponent<CollisionRetriever>();
        _controller = GetComponent<Controller>();

        _isJumpReset = true;
        _defaultGravityScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        _desiredJump = _controller.input.RetrieveJumpInput();
    }

    private void FixedUpdate()
    {
        _onGround = _collisionRetriever.OnGround;
        _onWall = _collisionRetriever.OnWall;
        _velocity = _body.velocity;

        if ((_onGround || _onWall) && _body.velocity.y == 0)
        {
            _jumpPhase = 0;
            _coyoteCounter = _coyoteTime;
            _isJumping = false;
        }
        else
        {
            _coyoteCounter -= Time.deltaTime;
        }

        if (_desiredJump && _isJumpReset)
        {
            _isJumpReset = false;
            _desiredJump = false;
            _jumpBufferCounter = _jumpBufferTime;
        }
        else if (_jumpBufferCounter > 0)
        {
            _jumpBufferCounter -= Time.deltaTime;
        }
        else if (!_desiredJump)
        {
            _isJumpReset = true;
        }

        if (_jumpBufferCounter > 0)
        {
            JumpAction();
        }

        if (_controller.input.RetrieveJumpInput() && _body.velocity.y > 0)
        {
            _body.gravityScale = _upwardMovementMultiplier;
        }
        else if (!_controller.input.RetrieveJumpInput() || _body.velocity.y < 0)
        {
            _body.gravityScale = _downwardMovementMultiplier;
        }
        else if (_body.velocity.y == 0)
        {
            _body.gravityScale = _defaultGravityScale;
        }

        _body.velocity = _velocity;
    }
    private void JumpAction()
    {
        if (_coyoteCounter > 0f || (_jumpPhase < _maxAirJumps && _isJumping))
        {
            if (_isJumping)
            {
                _jumpPhase += 1;
            }

            _jumpBufferCounter = 0;
            _coyoteCounter = 0;
            _jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * _jumpHeight);
            _isJumping = true;

            switch (_velocity.y)
            {
                case > 0f:
                    _jumpSpeed = Mathf.Max(_jumpSpeed - _velocity.y, 0f);
                    break;
                case < 0f:
                    _jumpSpeed += Mathf.Abs(_body.velocity.y);
                    break;
            }
            _velocity.y += _jumpSpeed;
        }
    }
}
