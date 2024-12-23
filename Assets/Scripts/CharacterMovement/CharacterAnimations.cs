using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterMovement
{
    public class CharacterAnimations : MonoBehaviour
    {
        // damping time smooths rapidly changing values sent to animator
        [SerializeField] protected float _dampTime = 0.1f;

        protected Animator _animator;
        protected CharacterMovementBase _characterMovement;

        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
            _characterMovement = GetComponentInParent<CharacterMovementBase>();
        }

        protected virtual void Update()
        {
            // send velocity to animator, ignoring y-velocity
            Vector3 velocity = _characterMovement.Velocity;
            Vector3 flattenedVelocity = new Vector3(velocity.x, 0f, velocity.z);
            float speed = Mathf.Min(_characterMovement.MoveInput.magnitude, flattenedVelocity.magnitude / _characterMovement.Speed);
            _animator.SetFloat("Speed", speed, _dampTime, Time.deltaTime);
            // send grounded state
            _animator.SetBool("IsGrounded", _characterMovement.IsGrounded);
            // send isolated y-velocity
            _animator.SetFloat("VerticalVelocity", velocity.y);
        }
    }
}
