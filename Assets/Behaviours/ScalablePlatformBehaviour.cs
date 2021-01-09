using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace Assets.Behaviours.Editor
{
    //    [ExecuteInEditMode]
    class ScalablePlatformBehaviour : MonoBehaviour
    {
        [SerializeField]
        bool LeftEdge = true;
        [SerializeField]
        bool RightEdge = true;
        [SerializeField]
        bool FrontEdge = false;
        [SerializeField]
        bool BackEdge = false;
        [SerializeField]
        int Width = 1;
        [SerializeField]
        int Length = 1;
        [SerializeField]
        GameObject Base;
        [SerializeField]
        GameObject LeftRim;
        [SerializeField]
        GameObject RightRim;
        [SerializeField]
        GameObject FrontRim;
        [SerializeField]
        GameObject BackRim;

        static void SetUnitObjectBetweenPositions(Vector3 p1, Vector3 p2, Transform tr)
        {
            Vector3 scale = p2 - p1;

            UnityEngine.Assertions.Assert.IsTrue(scale.x >= 0);
            UnityEngine.Assertions.Assert.IsTrue(scale.y >= 0);
            UnityEngine.Assertions.Assert.IsTrue(scale.z >= 0);

            Vector3 centre = (p1 + p2) / 2;

            tr.localScale = scale;
            tr.localPosition = centre;
        }

        void OnValidate()
        {
            LeftRim.SetActive(LeftEdge);
            RightRim.SetActive(RightEdge);
            FrontRim.SetActive(FrontEdge);
            BackRim.SetActive(BackEdge);

            float left_full = -Width / 2.0f;
            float left_adj = left_full + (LeftEdge ? 0.1f : 0.0f);
            float right_full = Width / 2.0f;
            float right_adj = right_full - (RightEdge ? 0.1f : 0.0f);
            float front_full = -Length / 2.0f;
            float front_adj = front_full + (FrontEdge ? 0.1f : 0.0f);
            float back_full = Length / 2.0f;
            float back_adj = back_full - (BackEdge ? 0.1f : 0.0f);

            SetUnitObjectBetweenPositions(new Vector3(left_adj, -0.05f, front_adj), new Vector3(right_adj, 0.05f, back_adj), Base.transform);
            SetUnitObjectBetweenPositions(new Vector3(left_full, -0.05f, front_full), new Vector3(left_adj, 0.15f, back_full), LeftRim.transform);
            SetUnitObjectBetweenPositions(new Vector3(right_adj, -0.05f, front_full), new Vector3(right_full, 0.15f, back_full), RightRim.transform);
            SetUnitObjectBetweenPositions(new Vector3(left_full, -0.05f, front_full), new Vector3(right_full, 0.15f, front_adj), FrontRim.transform);
            SetUnitObjectBetweenPositions(new Vector3(left_full, -0.05f, back_adj), new Vector3(right_full, 0.15f, back_full), BackRim.transform);
        }
    }
}
