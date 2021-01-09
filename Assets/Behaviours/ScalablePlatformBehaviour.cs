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
        float LeftEdge = 0.2f;
        [SerializeField]
        float RightEdge = 0.2f;
        [SerializeField]
        float FrontEdge = 0.0f;
        [SerializeField]
        float BackEdge = 0.0f;
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

            //UnityEngine.Assertions.Assert.IsTrue(scale.x >= 0);
            //UnityEngine.Assertions.Assert.IsTrue(scale.y >= 0);
            //UnityEngine.Assertions.Assert.IsTrue(scale.z >= 0);

            Vector3 centre = (p1 + p2) / 2;

            tr.localScale = scale;
            tr.localPosition = centre;
        }

        void OnValidate()
        {
            bool left_edge_on = LeftEdge > 0.1f;
            bool right_edge_on = RightEdge > 0.1f;
            bool front_edge_on = FrontEdge > 0.1f;
            bool back_edge_on = BackEdge > 0.1f;

            LeftRim.SetActive(left_edge_on);
            RightRim.SetActive(right_edge_on);
            FrontRim.SetActive(front_edge_on);
            BackRim.SetActive(back_edge_on);

            float left_full = -0.5f;
            float left_adj = left_full + (left_edge_on ? 0.1f : 0.0f);
            float right_full = Width -0.5f;
            float right_adj = right_full - (right_edge_on ? 0.1f : 0.0f);
            float front_full = -0.5f;
            float front_adj = front_full + (front_edge_on ? 0.1f : 0.0f);
            float back_full = Length -0.5f;
            float back_adj = back_full - (back_edge_on ? 0.1f : 0.0f);
            float bottom = 0.0f;
            float top = 0.1f;

            SetUnitObjectBetweenPositions(new Vector3(left_adj, bottom, front_adj), new Vector3(right_adj, top, back_adj), Base.transform);
            SetUnitObjectBetweenPositions(new Vector3(left_full, bottom, front_full), new Vector3(left_adj, top + LeftEdge - 0.1f, back_full), LeftRim.transform);
            SetUnitObjectBetweenPositions(new Vector3(right_adj, bottom, front_full), new Vector3(right_full, top + RightEdge - 0.1f, back_full), RightRim.transform);
            SetUnitObjectBetweenPositions(new Vector3(left_full, bottom, front_full), new Vector3(right_full, top + FrontEdge - 0.1f, front_adj), FrontRim.transform);
            SetUnitObjectBetweenPositions(new Vector3(left_full, bottom, back_adj), new Vector3(right_full, top + BackEdge - 0.1f, back_full), BackRim.transform);
        }
    }
}
