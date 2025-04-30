using System;
using UnityEngine;
using VRWeb.Rig;

namespace VRWeb.Events
{
	[CreateAssetMenu( fileName = "ViewModeChangedEvent", menuName = "VrWeb/ViewModeChangedEvent")]
    public class ViewModeChangedEvent : ScriptableObject
    {
        public event Action< ViewModes, ViewModes > onViewModeChanged;
        public void OnViewModeChanged( ViewModes oldMode, ViewModes newMode )
        {
            onViewModeChanged?.Invoke( oldMode, newMode );
        }
    }
}