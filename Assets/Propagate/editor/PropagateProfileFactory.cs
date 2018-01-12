using UnityEngine;
using UnityEditor;
using System.IO;

namespace Locogame.Propagate
{

    public class PropagateProfileFactory
    {
        [MenuItem("Assets/Create/PropagateProfile")]
        static void CreateProfile()
        {
            PropagateProfile asset = ScriptableObject.CreateInstance<PropagateProfile>();
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (path == "")
            {
                path = "Assets";
            }
            else if (Path.GetExtension(path) != "")
            {
                path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
            }

            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New " + typeof(PropagateProfile).ToString() + ".asset");

            AssetDatabase.CreateAsset(asset, assetPathAndName);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }
    }

}