using System;
using UnityEngine;

namespace VRWeb.Managers
{
    public interface IAssetBundleManager
    {
        public void RequestObjectFromAssetBundle < T >(
            string path,
            string objectName,
            uint version,
            Action < T > callback ) where T : UnityEngine.Object;
    }
}