using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Locogame.Propagate
{

    [CustomEditor(typeof(PropagateSound))]
    public class PropagateSoundEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            //PropagateLogoGUI.DrawLogo();
            base.OnInspectorGUI();
        }
    }

}