using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace Assets.Behaviours.Editor
{
    enum ColourScheme
    {
        BlackAndGold,
        RedAndBlack,
        GreenAndGold,
        WhiteAndBlack,
        RedAndGreen,
        BlueAndCyan,
        BlueAndGold,
        CyanAndBlack,
        BlueAndWhite,
        Goal
    }
    class ColourSchemeBehaviour : MonoBehaviour
    {
        [SerializeField]
        ColourScheme Scheme = ColourScheme.BlueAndGold;
        [SerializeField]
        bool Invert = false;
        [SerializeField]
        bool Override = false;

        static readonly Lazy<Material> Red = new Lazy<Material>(() => Resources.Load<Material>("Pipe Palette/BasicPipeRed"));
        static readonly Lazy<Material> Green = new Lazy<Material>(() => Resources.Load<Material>("Pipe Palette/BasicPipeGreen"));
        static readonly Lazy<Material> Blue = new Lazy<Material>(() => Resources.Load<Material>("Pipe Palette/BasicPipeBlue"));
        static readonly Lazy<Material> Cyan = new Lazy<Material>(() => Resources.Load<Material>("Pipe Palette/BasicPipeCyan"));
        static readonly Lazy<Material> Dark = new Lazy<Material>(() => Resources.Load<Material>("Pipe Palette/BasicPipeDark"));
        static readonly Lazy<Material> Light = new Lazy<Material>(() => Resources.Load<Material>("Pipe Palette/BasicPipeLight"));
        static readonly Lazy<Material> Orange = new Lazy<Material>(() => Resources.Load<Material>("Pipe Palette/BasicPipeOrange"));
        static readonly Lazy<Material> GoalGreen = new Lazy<Material>(() => Resources.Load<Material>("Pipe Palette/BasicPipeGoalGreen"));
        static readonly Lazy<Material> GoalOrange = new Lazy<Material>(() => Resources.Load<Material>("Pipe Palette/BasicPipeGoalOrange"));

        void GetMaterials(out Material main, out Material trim)
        {
            main = null;
            trim = null;

            switch(Scheme)
            {
                case ColourScheme.BlackAndGold:
                    main = Dark.Value;
                    trim = Orange.Value;
                    break;
                case ColourScheme.BlueAndCyan:
                    main = Blue.Value;
                    trim = Cyan.Value;
                    break;
                case ColourScheme.BlueAndGold:
                    main = Blue.Value;
                    trim = Orange.Value;
                    break;
                case ColourScheme.BlueAndWhite:
                    main = Blue.Value;
                    trim = Light.Value;
                    break;
                case ColourScheme.CyanAndBlack:
                    main = Cyan.Value;
                    trim = Dark.Value;
                    break;
                case ColourScheme.GreenAndGold:
                    main = Green.Value;
                    trim = Orange.Value;
                    break;
                case ColourScheme.RedAndBlack:
                    main = Red.Value;
                    trim = Dark.Value;
                    break;
                case ColourScheme.RedAndGreen:
                    main = Red.Value;
                    trim = Green.Value;
                    break;
                case ColourScheme.WhiteAndBlack:
                    main = Light.Value;
                    trim = Dark.Value;
                    break;
                case ColourScheme.Goal:
                    main = GoalGreen.Value;
                    trim = GoalOrange.Value;
                    break;
            }

            if (Invert)
            {
                var temp = main;
                main = trim;
                trim = temp;
            }
        }

        void OnValidate()
        {
            ColourSchemeBehaviour parent_csb = transform.parent != null ? transform.parent.gameObject.GetComponentInParent<ColourSchemeBehaviour>() : null;

            // if we are not an override then any parent takes precedence
            if (Override || parent_csb == null)
            {
                Material main, trim;
                GetMaterials(out main, out trim);

                RecurseChildren(transform, main, trim);
            }

            // if we (or anyone beneath us) changed our override
            // then a parent may need to reapply their materials
            if (parent_csb)
                parent_csb.OnValidate();
        }

        private void RecurseChildren(Transform trans, Material main, Material trim)
        {
            ColourSchemeBehaviour csb = trans.GetComponent<ColourSchemeBehaviour>();

            // if we're not looking at ourself and there is another scheme and it is set to override, nothing for us to do...
            if (csb && csb != this && csb.Override)
                return;

            MeshRenderer mr = trans.GetComponent<MeshRenderer>();

            if (mr)
            {
                var name = mr.gameObject.name;

                if (name.Contains("Trim") || name.Contains("Rim"))
                {
                    mr.sharedMaterials = new[] { trim };
                }
                else
                {
                    mr.sharedMaterials = new[] { main };
                }
            }

            foreach (Transform tr in trans)
            {
                RecurseChildren(tr, main, trim);
            }
        }
    }
}
