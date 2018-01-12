using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Locogame.Propagate
{

	public static class PropagateLogoGUI
	{
        static Texture logo;
		static GUIStyle _logoStyle;
		static GUIStyle logoStyle 
		{
			get
			{
				if (_logoStyle == null)
				{
					var style = new GUIStyle();
					style.alignment = TextAnchor.MiddleCenter;
					style.border = new RectOffset(3,3,3,3);
					style.margin = new RectOffset(5,5,5,5);
					style.stretchWidth = true;
					style.fixedHeight = 128;
					_logoStyle = style;
				}
				return _logoStyle;
			}
		}

		public static void DrawLogo()
		{
            if (logo == null)
            {
                string[] logoAssets = AssetDatabase.FindAssets("propagate_logo_wide_dark");
                if (logoAssets.Length > 0)
                {
                    logo = AssetDatabase.LoadAssetAtPath<Texture>(AssetDatabase.GUIDToAssetPath(logoAssets[0]));
                }
                else
                    return;
            }

			GUILayout.Box(logo, logoStyle);
		}
	}

}