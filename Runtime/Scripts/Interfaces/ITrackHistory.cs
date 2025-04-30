using UnityEngine;

namespace VRWeb.Tracking
{
    public interface ITrackHistory
    {
        public bool GetPositionOrientationForUrl( string url, out Vector3 position, out Vector3 orientation );
    }
}