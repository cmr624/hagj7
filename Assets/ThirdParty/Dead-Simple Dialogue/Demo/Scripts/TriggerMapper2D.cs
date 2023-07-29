using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Dossamer
{
    // useful for scripting zone-based events in-editor
    public class TriggerMapper2D : MonoBehaviour
    {
        [SerializeField]
        UnityEvent OnTriggerActivated;

        [SerializeField]
        UnityEvent OnTriggerDeactivated;

        [Header("Which transforms activate trigger?")]
        [SerializeField]
        List<Transform> _triggeringTransforms;
        
        // prevent double trigger (if player object has multiple colliders)
        HashSet<Transform> _currentTransforms;

        private void Awake()
        {
            _currentTransforms = new HashSet<Transform>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_currentTransforms.Contains(other.transform) && 
                _triggeringTransforms.Contains(other.transform))
            {
                _currentTransforms.Add(other.transform);
                OnTriggerActivated?.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_currentTransforms.Contains(other.transform))
            {
                _currentTransforms.Remove(other.transform);
                OnTriggerDeactivated?.Invoke();
            }
        }
    }
}