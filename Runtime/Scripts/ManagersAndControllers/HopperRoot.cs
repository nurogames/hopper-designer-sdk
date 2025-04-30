using System;
using System.Collections.Generic;
using UnityEngine;

namespace VRWeb.Managers
{
	public static class HopperRoot
    {
        public static T Get < T >()
        {
            Type usedType = typeof(T);

            if ( usedType.IsInterface )
            {
                foreach (KeyValuePair<Type, object> kvp in s_Managers)
                {
                    if ( kvp.Value is T value)
                        return value;
                }
            }
            
            return s_Managers.TryGetValue( usedType, out object manager ) ? ( T )manager : default( T );
        }

        private static Dictionary < Type, object > s_Managers = new();

        public static void RegisterManager < T >( HopperManagerMonoBehaviour < T > manager )
        {
            Type t = typeof( T );

            if ( s_Managers.ContainsKey( t ) )
                s_Managers.Remove( t );

            s_Managers.Add( t, manager );
        }

        public static void UnregisterManager < T >( HopperManagerMonoBehaviour < T > manager )
        {
            Type t = typeof( T );

            if ( s_Managers.ContainsKey( t ) )
                s_Managers.Remove( t );
        }
    }

    public class HopperManagerMonoBehaviour < T > : MonoBehaviour
    {
        protected void RegisterManager()
        {
            HopperRoot.RegisterManager < T >( this );
        }

        protected void UnregisterManager()
        {
            HopperRoot.UnregisterManager < T >( this );
        }
    }
}