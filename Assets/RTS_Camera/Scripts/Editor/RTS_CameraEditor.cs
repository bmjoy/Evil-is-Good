using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

namespace RTS_Cam
{
    [CustomEditor(typeof(RTSCamera))]
    public class RTS_CameraEditor : Editor
    {
        private RTSCamera Camera { get { return target as RTSCamera; } }

        private TabsBlock tabs;

        private void OnEnable()
        {
            tabs = new TabsBlock(new Dictionary<string, System.Action>() 
            {
                {"Movement", MovementTab},
                {"Rotation", RotationTab},
                {"Height", HeightTab}
            });
            tabs.SetCurrentMethod(Camera.lastTab);
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();
            Undo.RecordObject(Camera, "RTS_CAmera");
            tabs.Draw();
            if (GUI.changed)
                Camera.lastTab = tabs.curMethodIndex;
            EditorUtility.SetDirty(Camera);
        }

        private void MovementTab()
        {
            using (new HorizontalBlock())
            {
                GUILayout.Label("Use keyboard input: ", EditorStyles.boldLabel, GUILayout.Width(170f));
                Camera.useKeyboardInput = EditorGUILayout.Toggle( Camera.useKeyboardInput);
            }
            if(Camera.useKeyboardInput)
            {
                Camera.horizontalAxis = EditorGUILayout.TextField("Horizontal axis name: ", Camera.horizontalAxis);
                Camera.verticalAxis = EditorGUILayout.TextField("Vertical axis name: ", Camera.verticalAxis);
                Camera.keyboardMovementSpeed = EditorGUILayout.FloatField("Movement speed: ", Camera.keyboardMovementSpeed);
            }

            using (new HorizontalBlock())
            {
                GUILayout.Label("Screen edge input: ", EditorStyles.boldLabel, GUILayout.Width(170f));
                Camera.useScreenEdgeInput = EditorGUILayout.Toggle( Camera.useScreenEdgeInput);
            }

            if(Camera.useScreenEdgeInput)
            {
                EditorGUILayout.FloatField("Screen edge border size: ", Camera.screenEdgeBorder);
                Camera.screenEdgeMovementSpeed = EditorGUILayout.FloatField("Screen edge movement speed: ", Camera.screenEdgeMovementSpeed);
            }

            using (new HorizontalBlock())
            {
                GUILayout.Label("Panning with mouse: ", EditorStyles.boldLabel, GUILayout.Width(170f));
                Camera.usePanning = EditorGUILayout.Toggle(Camera.usePanning);
            }
            if(Camera.usePanning)
            {
                Camera.panningKey = (KeyCode)EditorGUILayout.EnumPopup("Panning when holding: ", Camera.panningKey);
                Camera.panningSpeed = EditorGUILayout.FloatField("Panning speed: ", Camera.panningSpeed);
            }

            using (new HorizontalBlock())
            {
                GUILayout.Label("Limit movement: ", EditorStyles.boldLabel, GUILayout.Width(170f));
                Camera.limitMap = EditorGUILayout.Toggle(Camera.limitMap);
            }
            if (Camera.limitMap)
            {
                Camera.limitX = EditorGUILayout.FloatField("Limit X: ", Camera.limitX);
                Camera.limitY = EditorGUILayout.FloatField("Limit Y: ", Camera.limitY);
            }

            GUILayout.Label("Follow target", EditorStyles.boldLabel);
            Camera.targetFollow = EditorGUILayout.ObjectField("Target to follow: ", Camera.targetFollow, typeof(Transform), true) as Transform;
            Camera.targetOffset = EditorGUILayout.Vector3Field("Target offset: ", Camera.targetOffset);
            Camera.followingSpeed = EditorGUILayout.FloatField("Following speed: ", Camera.followingSpeed);
        }

        private void RotationTab()
        {
            using (new HorizontalBlock())
            {
                GUILayout.Label("Keyboard input: ", EditorStyles.boldLabel, GUILayout.Width(170f));
                Camera.useKeyboardRotation = EditorGUILayout.Toggle(Camera.useKeyboardRotation);
            }
            if(Camera.useKeyboardRotation)
            {
                Camera.rotateLeftKey = (KeyCode)EditorGUILayout.EnumPopup("Rotate left: ", Camera.rotateLeftKey);
                Camera.rotateRightKey = (KeyCode)EditorGUILayout.EnumPopup("Rotate right: ", Camera.rotateRightKey);
                Camera.rotationSped = EditorGUILayout.FloatField("Keyboard rotation speed", Camera.rotationSped);
            }

            using (new HorizontalBlock())
            {
                GUILayout.Label("Mouse input: ", EditorStyles.boldLabel, GUILayout.Width(170f));
                Camera.useMouseRotation = EditorGUILayout.Toggle(Camera.useMouseRotation);
            }
            if(Camera.useMouseRotation)
            {
                Camera.mouseRotationKey = (KeyCode)EditorGUILayout.EnumPopup("Mouse rotation key: ", Camera.mouseRotationKey);
                Camera.mouseRotationSpeed = EditorGUILayout.FloatField("Mouse rotation speed: ", Camera.mouseRotationSpeed);
            }
        }

        private void HeightTab()
        {
            using (new HorizontalBlock())
            {
                GUILayout.Label("Auto height: ", EditorStyles.boldLabel, GUILayout.Width(170f));
                Camera.autoHeight = EditorGUILayout.Toggle(Camera.autoHeight);
            }
            if (Camera.autoHeight)
            {
                Camera.heightDampening = EditorGUILayout.FloatField("Height dampening: ", Camera.heightDampening);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("groundMask"));
            }

            using (new HorizontalBlock())
            {
                GUILayout.Label("Keyboard zooming: ", EditorStyles.boldLabel, GUILayout.Width(170f));
                Camera.useKeyboardZooming = EditorGUILayout.Toggle(Camera.useKeyboardZooming);
            }
            if(Camera.useKeyboardZooming)
            {
                Camera.zoomInKey = (KeyCode)EditorGUILayout.EnumPopup("Zoom In: ", Camera.zoomInKey);
                Camera.zoomOutKey = (KeyCode)EditorGUILayout.EnumPopup("Zoom Out: ", Camera.zoomOutKey);
                Camera.keyboardZoomingSensitivity = EditorGUILayout.FloatField("Keyboard sensitivity: ", Camera.keyboardZoomingSensitivity);
            }

            using (new HorizontalBlock())
            {
                GUILayout.Label("Scrollwheel zooming: ", EditorStyles.boldLabel, GUILayout.Width(170f));
                Camera.useScrollwheelZooming = EditorGUILayout.Toggle(Camera.useScrollwheelZooming);
            }
            if (Camera.useScrollwheelZooming)
                Camera.scrollWheelZoomingSensitivity = EditorGUILayout.FloatField("Scrollwheel sensitivity: ", Camera.scrollWheelZoomingSensitivity);

            if (Camera.useScrollwheelZooming || Camera.useKeyboardZooming)
            {
                using (new HorizontalBlock())
                {
                    Camera.maxHeight = EditorGUILayout.FloatField("Max height: ", Camera.maxHeight);
                    Camera.minHeight = EditorGUILayout.FloatField("Min height: ", Camera.minHeight);
                }
            }  
        }
    }
}