using UnityEngine;
using UnityEngine.Events;
using VRWeb.Managers;
using System.Collections;

namespace VRWeb.Creator
{
	public class ReadOutPersistentParam : MonoBehaviour
    {
        [SerializeField]
        private string m_Key;

        [SerializeField]
        private UnityEvent<string> m_Invoke = new UnityEvent<string>();

        IEnumerator Start()
        {
            yield return new WaitUntil( () => HopperRoot.Get<IPortalManager>() != null);

            m_Invoke.Invoke(HopperRoot.Get<IPortalManager>().GetParamAsString(m_Key));
        }
    }

}
