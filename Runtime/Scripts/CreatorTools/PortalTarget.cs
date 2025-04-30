using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using VRWeb.Managers;
using VRWeb.Rig;
using VRWeb.Tracking;

namespace VRWeb.Creator
{

	[ExecuteAlways]
    public class PortalTarget : MonoBehaviour
    {
        [SerializeField]
        private bool m_SimulateEnterWorld = false;

        [SerializeField]
        public UnityEvent OnEnterWorld = null;
        
        [SerializeField]
        private bool m_ReturnToExitPosition = true;
        
        private string m_Url;

#if UNITY_EDITOR
        private void OnValidate()
        {
            AddDefaultListeners();
        }
#endif
        private IEnumerator Start()
        {
            if ( !m_SimulateEnterWorld )
                yield break;

            yield return new WaitUntil(() => HopperRoot.Get < ITrackHistory >() != null);

            OnEnterWorld?.Invoke();
        }

        public void EnterWorldCallback()
        {
            if (m_ReturnToExitPosition)
            {
                ITrackHistory history = HopperRoot.Get<ITrackHistory>();

                if (history.GetPositionOrientationForUrl( 
                        m_Url, 
                        out Vector3 pos, 
                        out Vector3 dir ))
                {
                    HopperRoot.Get< IRigPositioner >().SetUserPositionAndForwardDirection(
                        pos, 
                        dir);
                    return;
                }
            }

            HopperRoot.Get < IRigPositioner >().SetUserPositionAndForwardDirection(transform.position, transform.forward);
        }

        public void EnterWorldAfterDeserialize( Transform posDir )
        {
            IRigPositioner rp = HopperRoot.Get < IRigPositioner >();
            rp.SetUserPositionAndForwardDirection( posDir.position, posDir.forward );
        }

        public void EnterWorld(string rootUrl)
        {
            m_Url = rootUrl;

            OnEnterWorld?.Invoke();
		}

        protected virtual void AddDefaultListeners()
        {
#if UNITY_EDITOR
            if (OnEnterWorld != null && OnEnterWorld.GetPersistentEventCount() > 0)
            {
                return;
            }

            PortalTarget[] otherTarget = FindObjectsByType<PortalTarget>( FindObjectsSortMode.None );
            if (otherTarget.Length > 1)
            {
                Debug.LogWarning(
                    $"This scene now contains {otherTarget.Length} instances of PortalTarget,\n but it should only contain exactly one:");

                foreach (var t in otherTarget)
                {
                    Debug.LogWarning($"-- attached to gameobject \"{t.gameObject.name}\" in scene {t.gameObject.scene.name}", t.gameObject);
                }
            }

            UnityAction methodDelegate =
                System.Delegate.CreateDelegate(
                        typeof(UnityAction),
                        this,
                        "EnterWorldCallback")
                    as UnityAction;

            OnEnterWorld = new UnityEvent();

            UnityEditor.Events.UnityEventTools.AddPersistentListener(OnEnterWorld, methodDelegate);
#endif
        }

        public void PositionRig()
        {
			OnEnterWorld?.Invoke();
		}
    }
}
