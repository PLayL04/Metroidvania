using UnityEngine;

[RequireComponent(typeof(Controller))]
public class Move : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float _maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float _maxAcceleration = 35f;
    [SerializeField, Range(0f, 100f)] private float _maxAirAcceleration = 20f;
    [SerializeField, Range(0f, 100f)] private float _wallStickTime = 0.25f;

    private Controller _controller;
    private Vector2 _direction, _desiredVelocity, _velocity;
    private Rigidbody2D _body;
    private CollisionRetriever _collisionRetriever;
    private WallInteractor _wallInteractor;

    private float _maxSpeedChange, _acceleration, _wallStickCounter;
    private bool _onGround;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _collisionRetriever = GetComponent<CollisionRetriever>();
        _controller = GetComponent<Controller>();
        _wallInteractor = GetComponent<WallInteractor>();
    }

    private void Update()
    {
        _direction.x = _controller.input.RetrieveMoveInput();
        _desiredVelocity = new Vector2(_direction.x, 0f) * Mathf.Max(_maxSpeed - _collisionRetriever.Friction, 0f);
    }

    private void FixedUpdate()
    {
        _onGround = _collisionRetriever.OnGround;
        _velocity = _body.velocity;

        _acceleration = _onGround ? _maxAcceleration : _maxAirAcceleration;
        _maxSpeedChange = _acceleration * Time.deltaTime;
        _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, _maxSpeedChange);

        if (_collisionRetriever.OnWall && !_collisionRetriever.OnGround && !_wallInteractor.WallJumping)
        {
            if (_wallStickCounter > 0)
            {
                _velocity.x = 0;

                if (_controller.input.RetrieveMoveInput() == _collisionRetriever.ContactNormal.x)
                {
                    _wallStickCounter -= Time.deltaTime;
                }
                else
                {
                    _wallStickCounter = _wallStickTime;
                }
            }
            else
            {
                _wallStickCounter = _wallStickTime;
            }
        }

        _body.velocity = _velocity;
    }
}