using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRWeb.Creator
{
	public class DelayedActivate : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> m_ObjectsToActivate = null;

        [SerializeField]
        private int m_WaitFrames;

        private IEnumerator Start()
        {
            for (int i = 0; i < m_WaitFrames; i++)
                yield return new WaitForEndOfFrame();

            foreach (GameObject o in m_ObjectsToActivate)
            {
                o.SetActive(true);
            }
        }
    }
}