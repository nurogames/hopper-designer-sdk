using UnityEngine;

namespace VRWeb.Rig
{
    public interface IRigPositioner
    {
        public void SetUserPositionAndForwardDirection( Vector3 pos, Vector3 forward );
    }

}