using UnityEngine;
using UnityEngine.Events;

namespace VRWeb.Creator
{
	public class TriggerForwarder : MonoBehaviour
    {
        public enum TriggerType { enter, stay, exit };

        [SerializeField]
        public UnityEvent < Collider, TriggerType > onTrigger;

        [SerializeField]
        public UnityEvent < Collider > onTriggerEnter;

        [SerializeField]
        public UnityEvent < Collider > onTriggerStay;

        [SerializeField]
        public UnityEvent < Collider > onTriggerExit;

        private void OnTriggerEnter( Collider collider )
        {
            onTrigger?.Invoke( collider, TriggerType.enter );
            onTriggerEnter?.Invoke( collider );
        }

        private void OnTriggerStay( Collider collider )
        {
            onTrigger?.Invoke( collider, TriggerType.stay );
            onTriggerStay?.Invoke( collider );
        }

        private void OnTriggerExit( Collider collider )
        {
            onTrigger?.Invoke( collider, TriggerType.exit );
            onTriggerExit?.Invoke( collider );
        }
    }
}