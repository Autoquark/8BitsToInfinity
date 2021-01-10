using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Extensions
{
    static class ComponentExtensions
    {
        public static void DestroyGameObject(this Component component)
        {
            if(component == null)
            {
                return;
            }
            UnityEngine.Object.Destroy(component.gameObject);
        }

        /// <summary>
        /// Returns whether a GameObject has been destroyed or set inactive
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        /// <remarks>
        /// When simulating, no unity updates occur between steps, which means that destroyed objects do not actually get destroyed. Therefore we set them as inactive in addition to calling Destroy.
        /// N.B. This method uses the unity == overload to detect destroyed objects, which seems to be the only way :(
        /// </remarks>
        public static bool IsNullDestroyedOrGameObjectInactive(this Component component) => component == null || !component.gameObject.activeInHierarchy;

        public static bool HasComponent<T>(this Component component) => component.GetComponent<T>() != null;
    }
}
