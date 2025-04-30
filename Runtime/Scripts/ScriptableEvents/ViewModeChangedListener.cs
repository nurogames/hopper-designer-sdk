using UnityEngine;
using UnityEngine.Events;
using VRWeb.Rig;

namespace VRWeb.Events
{
	public class ViewModeChangedListener : MonoBehaviour
    {
        [SerializeField]
        private ViewModeChangedEvent m_Event = null;

        [SerializeField]
        private UnityEvent<ViewModes, ViewModes> m_OnInvoke = 
            new UnityEvent <ViewModes, ViewModes >();

        [SerializeField]
        private UnityEvent m_OnInvokeVrMode = new UnityEvent();

        [SerializeField]
        private UnityEvent m_OnInvokeScreenMode = new UnityEvent();

        void OnEnable()
        {
            m_Event.onViewModeChanged += Invoke;
        }

        void OnDisable()
        {
            m_Event.onViewModeChanged -= Invoke;
        }

        void Invoke( ViewModes oldMode, ViewModes newMode )
        {
            m_OnInvoke?.Invoke( oldMode, newMode );

            switch ( newMode )
            {
                case ViewModes.FirstPerson:
                case ViewModes.ThirdPerson:
                    if ( oldMode == ViewModes.Vr )
                        m_OnInvokeScreenMode?.Invoke();
                    break;

                case ViewModes.Vr:
                    if ( oldMode != ViewModes.Vr )
                        m_OnInvokeVrMode?.Invoke();

                    break;
            }
        }
#if UNITY_EDITOR
        private void OnValidate()
        {
            if ( m_Event != null )
            {
                return;
            }

            m_Event = Resources.Load<ViewModeChangedEvent>("ViewModeChangedEvent");
        }
#endif

    }
}