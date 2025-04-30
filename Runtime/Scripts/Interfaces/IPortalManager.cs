using UnityEngine;

namespace VRWeb.Managers
{
    public interface IPortalManager
    {
        string GetParamAsString(string key, string defaultValue = "");
    }
}