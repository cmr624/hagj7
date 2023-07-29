using System;
using UnityEngine;

namespace Dossamer.Demo
{
    public class ConditionalFollow : MonoBehaviour
    {
        public bool ShouldFollowTarget = false;

        [SerializeField] private Transform _target;

        public void SetShouldFollowTarget(bool value)
        {
            ShouldFollowTarget = value; 
        }

        public void SetTargetTransform(Transform target)
        {
            _target = target; 
        }
        
        private void Update()
        {
            if (!_target || !ShouldFollowTarget) return;

            transform.position = Vector3.Lerp(transform.position, _target.position, Time.deltaTime);
        }
    }
}