using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace Assets.Behaviours.Editor
{
    //    [ExecuteInEditMode]
    class SlopeRoundedScalablePlatformBehaviour : MonoBehaviour
    {
        //[SerializeField]
        //float LeftEdge = 0.2f;
        //[SerializeField]
        //float RightEdge = 0.2f;
        [SerializeField]
        float LeftExtend = 0;
        [SerializeField]
        float RightExtend = 0;
        [SerializeField]
        GameObject Base;
        [SerializeField]
        GameObject LeftRim;
        [SerializeField]
        GameObject RightRim;
        //[SerializeField]
        //GameObject LeftUpstand;
        //[SerializeField]
        //GameObject RightUpstand;

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

            //LeftEdge = Mathf.Max(LeftEdge, 0.5f);
            //RightEdge = Mathf.Max(RightEdge, 0.5f);

            float base_width = LeftExtend + RightExtend;

            bool base_on = base_width > 0;

            //bool left_up_on = LeftEdge > 0.5f;
            //bool right_up_on = RightEdge > 0.5f;

            Base.SetActive(base_on);
            //LeftUpstand.SetActive(left_up_on);
            //RightUpstand.SetActive(right_up_on);

            float left_full = -0.5f - LeftExtend;
            float left_adj = -LeftExtend;
            float right_full = 0.5f + RightExtend;
            float right_adj = RightExtend;
            float front_full = -0.5f;
            float back_full = 0.5f;
            float bottom = 0.0f;
            float top = 0.1f;
            float rim_top = 0.5f;

            SetUnitObjectBetweenPositions(new Vector3(left_adj, bottom, front_full), new Vector3(right_adj, top, back_full), Base.transform);
            // left_adj and left_full are the other was around here, to mirror the mesh, which is required when it is asymetrical...
            SetUnitObjectBetweenPositions(new Vector3(left_adj, bottom, front_full), new Vector3(left_full, rim_top, back_full), LeftRim.transform);
            SetUnitObjectBetweenPositions(new Vector3(right_adj, bottom, front_full), new Vector3(right_full, rim_top, back_full), RightRim.transform);
            //SetUnitObjectBetweenPositions(new Vector3(left_full, rim_top, front_full), new Vector3(left_full + 0.1f, LeftEdge, back_full), LeftUpstand.transform);
            //SetUnitObjectBetweenPositions(new Vector3(right_full - 0.1f, rim_top, front_full), new Vector3(right_full, RightEdge, back_full), RightUpstand.transform);
        }
    }
}
