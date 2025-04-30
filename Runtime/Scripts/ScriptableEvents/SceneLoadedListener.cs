using UnityEngine;
using UnityEngine.Events;

namespace VRWeb.Events
{
	public class SceneLoadedListener : MonoBehaviour
    {
        [SerializeField]
        private SceneLoadedEvent m_Event = null;

        [SerializeField]
        private UnityEvent < SceneLoadedEvent.LoadStatus > m_OnInvoke = new UnityEvent < SceneLoadedEvent.LoadStatus >();

        void OnEnable()
        {
            m_Event.onSceneLoaded += Invoke;
        }

        void OnDisable()
        {
            m_Event.onSceneLoaded -= Invoke;
        }

        void Invoke( SceneLoadedEvent.LoadStatus status )
        {
            m_OnInvoke?.Invoke( status );
        }
#if UNITY_EDITOR
        private void OnValidate()
        {
            if ( m_Event != null )
            {
                return;
            }

            m_Event = Resources.Load< SceneLoadedEvent >("SceneLoadedEvent");
        }
#endif

    }

}