using System;
using UnityEngine;

namespace VRWeb.Events
{
	[CreateAssetMenu( fileName = "SceneLoadedEvent", menuName = "VrWeb/SceneLoadedEvent" )]
    public class SceneLoadedEvent : ScriptableObject
    {
        public enum LoadStatus { beginLoad, cancelLoad, finishLoad };
        public event Action < LoadStatus > onSceneLoaded;

        private LoadStatus m_Status = LoadStatus.finishLoad;

        public LoadStatus Status => m_Status;

        public void OnSceneLoaded( LoadStatus loadStatus = LoadStatus.finishLoad )
        {
            onSceneLoaded?.Invoke( loadStatus );
        }
    }
}