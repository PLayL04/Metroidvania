using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _body;
    [SerializeField] private Animator _animator;

    private bool _isWalking;
    private bool _isJumping;
    private Vector2 _velocity;

    void Update()
    {
        // Время говнокодинга
        _velocity = _body.velocity;
        if (_velocity.x != 0 && _velocity.y == 0)
        {
            _isWalking = true;
            _isJumping = false;
        }
        else if (_velocity.x == 0 && _velocity.y != 0)
        {
            _isWalking = false;
            _isJumping = true;
        }
        else if (_velocity.x != 0 && _velocity.y != 0)
        {
            _isWalking = true;
            _isJumping = true;
        }
        else
        {
            _isWalking = false;
            _isJumping = false;
        }

        _animator.SetBool("isWalking", _isWalking);
        _animator.SetBool("isJumping", _isJumping);
    }
}
