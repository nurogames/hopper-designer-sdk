using System;
using UnityEngine;
using VRWeb.Managers;

namespace VRWeb.Creator
{
	public class AssetSpawner : MonoBehaviour
	{
		[SerializeField]
		private bool m_SpawnOnStart = true;

		[SerializeField]
		private string m_AssetBundlePath;

		[SerializeField]
		private string m_PrefabName;

		private static readonly Vector3 V3ZERO = new Vector3(0, 0, 0);
		private static readonly Quaternion QID = new Quaternion(0, 0, 0, 1);

		void Start()
		{
			if (m_SpawnOnStart)
				SpawnPrefab();
		}

		public void SpawnPrefab()
		{
			SpawnPrefab(m_AssetBundlePath, m_PrefabName, transform, (g) => { });
		}

		public static void SpawnPrefab(
			string bundlePath,
			string prefabName,
			Transform parent,
			Action<GameObject> instantiateAction)
		{
			Debug.Log($"RequestObjectFromAssetBundle({bundlePath}, {prefabName})");

			HopperRoot.Get<IAssetBundleManager>().RequestObjectFromAssetBundle<GameObject>(bundlePath, prefabName, 1,
                (prefabGO) =>
				{
					GameObject go = Instantiate<GameObject>(prefabGO, parent);
					Debug.Log($"Instantiate({prefabGO.name} under {parent.name})");

					Transform t = go.transform;
					instantiateAction?.Invoke(go);
				});
		}
	}
}