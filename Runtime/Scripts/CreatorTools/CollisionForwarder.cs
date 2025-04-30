using UnityEngine;
using UnityEngine.Events;

namespace VRWeb.Creator
{
	public class CollisionForwarder : MonoBehaviour
    {
        public enum CollisionType { enter, stay, exit };

        [SerializeField]
        public UnityEvent < Collision, CollisionType > onCollision;

        [SerializeField]
        public UnityEvent < Collision > onCollisionEnter;
                                         
        [SerializeField]                 
        public UnityEvent < Collision > onCollisionStay;
                                         
        [SerializeField]                 
        public UnityEvent < Collision > onCollisionExit;

        private void OnCollisionEnter( Collision collision )
        {
            onCollision?.Invoke( collision, CollisionType.enter );
            onCollisionEnter?.Invoke( collision );
        }

        private void OnCollisionStay( Collision collision )
        {
            onCollision?.Invoke( collision, CollisionType.stay );
            onCollisionStay?.Invoke( collision );
        }

        private void OnCollisionExit( Collision collision )
        {
            onCollision?.Invoke( collision, CollisionType.exit );
            onCollisionExit?.Invoke( collision );
        }
    }
}