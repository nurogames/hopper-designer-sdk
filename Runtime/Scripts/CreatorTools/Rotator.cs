using UnityEngine;

namespace VRWeb.Creator
{
	public class Rotator : MonoBehaviour
	{
		[SerializeField] Vector3 m_RotationSpeed = Vector3.up * 90.0f;

		Transform trans;

		void Awake()
		{
			trans = transform;
		}

		// Update is called once per frame
		void Update()
		{
			trans.Rotate(m_RotationSpeed * Time.deltaTime, Space.Self);
		}
	}
}
