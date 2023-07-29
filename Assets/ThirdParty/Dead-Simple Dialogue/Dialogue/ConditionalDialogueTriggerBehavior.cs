using UnityEngine;
using Dossamer.Dialogue.Schema;

namespace Dossamer.Dialogue
{
    public class ConditionalDialogueTriggerBehavior : MonoBehaviour
    {
        // holds dialogue lines; passes them to dialogue manager when triggered

        [Header("If CustomConditional is true, trigger A; otherwise B.")]
        
        public Cutscene dialogueAToTrigger;
        public Cutscene dialogueBToTrigger;

        [SerializeField]
        public bool customConditional = false; 

        [SerializeField]
        bool _onlyTriggerAOnce = true;
        
        [SerializeField]
        bool _onlyTriggerBOnce = true;

        bool _isATriggered = false;
        bool _isBTriggered = false;

        public void SetConditional(bool value)
        {
            customConditional = value; 
        }
        
        public void TriggerDialogue()
        {
            if (_isATriggered && _isBTriggered) return;

            if (!customConditional)
            {
                if (_onlyTriggerAOnce && _isATriggered) return;
                
                Debug.Log("triggering A dialogue");
                _isATriggered = true;
                DialogueManager.Instance.StartNewDialogue(dialogueAToTrigger);
            }
            else
            {
                if (_onlyTriggerBOnce && _isBTriggered) return;
                
                Debug.Log("triggering B dialogue");
                _isBTriggered = true;
                DialogueManager.Instance.StartNewDialogue(dialogueBToTrigger);
            }
        }
    }
}