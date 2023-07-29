using UnityEngine;
using System;
using System.Collections.Generic;
using Dossamer.Dialogue.Schema;
using Dossamer.Utilities;
using UnityEngine.Events;

namespace Dossamer.Dialogue
{
    public class DialogueCallbackBank : MonoBehaviour
    {
        [Header("Link in-scene callbacks to cutscene asset lines via GUIDs.")]
        [SerializeField]
        private List<CallbackMapPair> _callbacksList;
        
        [Serializable]
        private struct CallbackMapPair
        {
            public string Label;
            public SerializableGuid CallingGuid;
            public UnityEvent Callback;
            
            [Header("Below only relevant to cutscene triggers.")]
            public bool ShouldTriggerOnEnd; 
        }

        private Dictionary<SerializableGuid, CallbackMapPair> _callbackMap;

        private void Awake()
        {
            _callbackMap = BuildMap(); 
        }

        private void Start()
        {
            DialogueManager.Instance.OnDialogueLineProgressed += HandleLine;
            DialogueManager.Instance.OnDialogueStarted += HandleCutsceneStarted;
            DialogueManager.Instance.OnDialogueEnded += HandleCutsceneEnded;
        }

        private void OnDisable()
        {
            DialogueManager.Instance.OnDialogueLineProgressed -= HandleLine;
            DialogueManager.Instance.OnDialogueStarted -= HandleCutsceneStarted;
            DialogueManager.Instance.OnDialogueEnded -= HandleCutsceneEnded;
        }

        private void HandleLine(Line line)
        {
            if (!_callbackMap.ContainsKey(line.Guid)) return; 
            
            _callbackMap[line.Guid].Callback?.Invoke();
        }

        private void HandleCutsceneStarted(Cutscene cutscene)
        {
            if (!_callbackMap.ContainsKey(cutscene.Guid)) return;

            var callback = _callbackMap[cutscene.Guid];

            if (callback.ShouldTriggerOnEnd) return;
            
            callback.Callback?.Invoke();
        }

        private void HandleCutsceneEnded(Cutscene cutscene)
        {
            if (!_callbackMap.ContainsKey(cutscene.Guid)) return;

            var callback = _callbackMap[cutscene.Guid];

            if (!callback.ShouldTriggerOnEnd) return;
            
            callback.Callback?.Invoke();
        }

        private Dictionary<SerializableGuid, CallbackMapPair> BuildMap()
        {
            var dict = new Dictionary<SerializableGuid, CallbackMapPair>();

            try
            {
                foreach (var pair in _callbacksList)
                {
                    dict[pair.CallingGuid] = pair;
                }
            } catch {}

            return dict; 
        }
        
        #if UNITY_EDITOR
        private void OnValidate()
        {
            _callbackMap = BuildMap(); 
        }
        #endif
    }
}