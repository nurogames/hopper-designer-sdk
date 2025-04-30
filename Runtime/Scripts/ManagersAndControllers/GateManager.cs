using System.Collections.Generic;
using VRWeb.Creator;

namespace VRWeb.Managers
{
	public class GateManager : HopperManagerMonoBehaviour<GateManager>
    {
        private Dictionary<int, Gate> m_GateDict = new Dictionary<int, Gate>();

        void Awake()
        {
            RegisterManager();
        }

        public void RegisterGate(Gate gate)
        {
            while (m_GateDict.ContainsKey(gate.ID))
            {
                foreach (var g in m_GateDict)
                {
                    g.Value.ID += 1000;
                    g.Value.TargetID += 1000;
                }
            }
            m_GateDict.Add(gate.ID, gate);
        }

        public void UnregisterGate(Gate gate)
        {
            if (m_GateDict.ContainsKey(gate.ID))
            {
                m_GateDict.Remove(gate.ID);
            }
        }

        public Gate GetGate(int index)
        {
            if (m_GateDict.ContainsKey(index))
                return m_GateDict[index];

            return null;
        }

        public void ClearAllGates()
        {
            m_GateDict.Clear();
        }
    }
}