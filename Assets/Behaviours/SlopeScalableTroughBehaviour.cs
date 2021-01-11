﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

enum SideType
{
    None,
    Low,
    High
}

namespace Assets.Behaviours.Editor
{
    class SlopeScalableTroughBehaviour : MonoBehaviour
    {
        [SerializeField]
        SideType LeftEdge = SideType.None;
        [SerializeField]
        SideType RightEdge = SideType.None;
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

        [SerializeField]
        static Lazy<Mesh> LowEdge = new Lazy<Mesh>(() => Resources.Load<Mesh>("LowEdge"));
        [SerializeField]
        static Lazy<Mesh> HighEdge = new Lazy<Mesh>(() => Resources.Load<Mesh>("HighEdge"));

        static void SetUnitObjectBetweenPositions(Vector3 p1, Vector3 p2, Transform tr)
        {
            Vector3 scale = p2 - p1;

            Vector3 centre = (p1 + p2) / 2;

            tr.localScale = scale;
            tr.localPosition = centre;
        }

        static void SetUnitObjectBetweenPositionsIgnoreY(Vector3 p1, Vector3 p2, Transform tr)
        {
            Vector3 scale = p2 - p1;
            scale.y = 1;

            Vector3 centre = (p1 + p2) / 2;

            tr.localScale = scale;
            tr.localPosition = centre;
        }

        void SetMesh(Transform tr, SideType st)
        {
            var mf = tr.GetComponent<MeshFilter>();

            switch(st)
            {
                case SideType.None:
                    break;

                case SideType.Low:
                    mf.mesh = LowEdge.Value;
                    break;

                case SideType.High:
                    mf.mesh = HighEdge.Value;
                    break;
            }
        }

        void OnValidate()
        {
            LeftExtend = Mathf.Max(0.0f, LeftExtend);
            RightExtend = Mathf.Max(0.0f, RightExtend);

            bool left_edge_on = LeftEdge != SideType.None;
            bool right_edge_on = RightEdge != SideType.None;

            LeftRim.SetActive(left_edge_on);
            RightRim.SetActive(right_edge_on);

            SetMesh(transform.Find("LeftRim"), LeftEdge);
            SetMesh(transform.Find("RightRim"), RightEdge);

            float width = LeftExtend + RightExtend + 1.0f;

            float left_full = -0.5f - LeftExtend;
            float left_adj = left_full + (left_edge_on ? 0.05f * width : 0.0f);
            float right_full = 0.5f + RightExtend;
            float right_adj = right_full - (right_edge_on ? 0.05f * width : 0.0f);
            float front_full = -0.5f;
            float back_full = 0.5f;
            float bottom = -0.9f;
            float top = 0.1f;

            SetUnitObjectBetweenPositions(new Vector3(left_full, bottom, front_full), new Vector3(right_full, top, back_full), Base.transform);
            LeftRim.transform.position = new Vector3(left_full, top, back_full);
            RightRim.transform.position = new Vector3(right_full - 0.05f, top, back_full);
        }
    }
}
