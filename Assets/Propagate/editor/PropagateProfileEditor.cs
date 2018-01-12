using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Locogame.Propagate
{

    [CustomEditor(typeof(PropagateProfile))]
    public class PropagateProfileEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            PropagateLogoGUI.DrawLogo();
            base.OnInspectorGUI();
        }
    }

}