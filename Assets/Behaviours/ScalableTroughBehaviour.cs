using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace Assets.Behaviours.Editor
{
    class ScalableTroughBehaviour : MonoBehaviour
    {
        [SerializeField]
        float LeftEdge = 0.2f;
        [SerializeField]
        float RightEdge = 0.2f;
        [SerializeField]
        float LeftExtend = 0;
        [SerializeField]
        float RightExtend = 0;
        [SerializeField]
        float FrontExtend = 0;
        [SerializeField]
        float BackExtend = 0;
        [SerializeField]
        GameObject Base;
        [SerializeField]
        GameObject LeftRim;
        [SerializeField]
        GameObject RightRim;

        static void SetUnitObjectBetweenPositions(Vector3 p1, Vector3 p2, Transform tr)
        {
            Vector3 scale = p2 - p1;

            Vector3 centre = (p1 + p2) / 2;

            tr.localScale = scale;
            tr.localPosition = centre;
        }

        void OnValidate()
        {
            LeftExtend = Mathf.Max(0.0f, LeftExtend);
            RightExtend = Mathf.Max(0.0f, RightExtend);
            FrontExtend = Mathf.Max(0.0f, FrontExtend);
            BackExtend = Mathf.Max(0.0f, BackExtend);

            bool left_edge_on = LeftEdge > 0.1f;
            bool right_edge_on = RightEdge > 0.1f;

            LeftRim.SetActive(left_edge_on);
            RightRim.SetActive(right_edge_on);

            float width = LeftExtend + RightExtend + 1.0f;

            float left_full = -0.5f - LeftExtend;
            float left_adj = left_full + (left_edge_on ? 0.05f * width : 0.0f);
            float right_full = 0.5f + RightExtend;
            float right_adj = right_full - (right_edge_on ? 0.05f * width : 0.0f);
            float front_full = -0.5f - FrontExtend;
            float back_full = 0.5f + BackExtend;
            float bottom = -0.9f;
            float top = 0.1f;

            SetUnitObjectBetweenPositions(new Vector3(left_full, bottom, front_full), new Vector3(right_full, top, back_full), Base.transform);
            SetUnitObjectBetweenPositions(new Vector3(left_full, top, front_full), new Vector3(left_adj, top + LeftEdge - 0.1f, back_full), LeftRim.transform);
            SetUnitObjectBetweenPositions(new Vector3(right_adj, top, front_full), new Vector3(right_full, top + RightEdge - 0.1f, back_full), RightRim.transform);
        }
    }
}
