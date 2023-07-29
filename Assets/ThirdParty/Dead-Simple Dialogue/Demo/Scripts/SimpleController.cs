using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

namespace Dossamer.Demo
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SimpleController : MonoBehaviour
    {
        [SerializeField]
        private bool _isMovementEnabled = true;

        public bool IsMovementEnabled
        {
            get => _isMovementEnabled;
            set => _isMovementEnabled = value;
        }

        [SerializeField] private float _horizontalAcceleration = 20f;
        [SerializeField] private float _maxHorizontalSpeed = 3.5f; 
        
        private Rigidbody2D _rigidbody;
        private Vector2 _moveDirection = Vector2.zero;
        
        // Start is called before the first frame update
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>(); 
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (!_isMovementEnabled || Mathf.Abs(_rigidbody.velocity.x) > _maxHorizontalSpeed) return;

            var magnitude = Vector2.Dot(Vector2.right, _moveDirection);
            _rigidbody.AddForce(_rigidbody.mass * magnitude * _horizontalAcceleration * Vector2.right);
        }

        public void Move(InputAction.CallbackContext context)
        {
            if (_isMovementEnabled && context.performed)
            {
                _moveDirection = context.ReadValue<Vector2>();
            }
            else if (!_isMovementEnabled || context.canceled)
            {
                _moveDirection = Vector2.zero;
            }
        }
    }
}