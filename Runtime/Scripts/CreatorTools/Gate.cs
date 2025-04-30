using UnityEngine;
using VRWeb.Managers;
using VRWeb.Rig;

namespace VRWeb.Creator
{
	public class Gate : MonoBehaviour
    {
        public int ID;
        public int TargetID;

        private void OnEnable()
        {
            HopperRoot.Get<GateManager>().RegisterGate(this);
        }

        private void OnDisable()
        {
            if (HopperRoot.Get<GateManager>())
                HopperRoot.Get<GateManager>().UnregisterGate(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            Gate gate = HopperRoot.Get<GateManager>().GetGate(TargetID);

            if (gate)
            {
                HopperRoot.Get<IRigPositioner>().SetUserPositionAndForwardDirection(
                    gate.transform.position + gate.transform.forward,
                    gate.transform.forward);
            }
        }
    }
}