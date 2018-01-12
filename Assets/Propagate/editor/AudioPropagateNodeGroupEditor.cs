using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Locogame.Propagate
{

    [CustomEditor(typeof(AudioPropagateNodeGroup))]
    public class AudioPropagateNodeGroupEditor : Editor
    {
        Node selectedNode;
        bool isLinking;

        [MenuItem("GameObject/Audio/Propagate Node Group")]
        public static void CreateAudioPropagateGroup(MenuCommand mCommand)
        {
            GameObject go = new GameObject("Audio Propagate Node Group");
            var group = go.AddComponent<AudioPropagateNodeGroup>();
            GameObjectUtility.SetParentAndAlign(go, mCommand.context as GameObject);
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go;
        }

        [MenuItem("GameObject/Audio/Propagate Sound")]
        public static void CreatePropagateSound(MenuCommand mCommand)
        {
            string[] foundAssets = AssetDatabase.FindAssets("PropagateSound t:prefab");
            if (foundAssets.Length > 0)
            {
                string psoundAssetPath = AssetDatabase.GUIDToAssetPath(foundAssets[0]);
                GameObject psoundPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(psoundAssetPath);
                GameObject psound = (GameObject)PrefabUtility.InstantiatePrefab(psoundPrefab);
                GameObjectUtility.SetParentAndAlign(psound, Selection.activeGameObject);
                Undo.RegisterCreatedObjectUndo(psound, "Create PropagateSound Object");
                Selection.activeObject = psound;
            }
            else
            {
                Debug.LogError("No PropagateSound prefab found! Please re-import Propagate.");
            }
        }

        Tool _lastTool = Tool.None;
        void OnEnable()
        {
            _lastTool = Tools.current;
        }

        void OnDisable()
        {
            Tools.current = _lastTool;
        }

        void OnSceneGUI()
        {
            Tools.current = Tool.None;
            AudioPropagateNodeGroup pGroup = (AudioPropagateNodeGroup)target;
            Handles.color = new Color(0, .5f, 1f, .5f);

            if (pGroup.nodes != null && pGroup.nodeCount > 0)
            {
                for (int nodeIndex = 0; nodeIndex < pGroup.nodeCount; ++nodeIndex)
                {
                    DrawNode(pGroup.nodes[nodeIndex], nodeIndex);
                }
            }

            if (selectedNode != null)
            {
                UpdateNode(selectedNode);
                if (Event.current.commandName == "FrameSelected")
                {
                    SceneView.lastActiveSceneView.LookAt(selectedNode.position, SceneView.lastActiveSceneView.rotation, 5.0f);
                    Event.current.Use();
                }

                if (Event.current.type == EventType.ExecuteCommand && Event.current.commandName == "Duplicate")
                {
                    selectedNode = pGroup.AddNode(selectedNode.position);
                    Event.current.Use();
                }

                if (Event.current.type == EventType.ValidateCommand && Event.current.commandName == "Delete")
                {
                    pGroup.DeleteNode(selectedNode);
                    selectedNode = null;
                    Event.current.commandName = "";
                    Event.current.Use();
                }

                if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.C)
                {
                    Debug.Log(selectedNode.connections.Count);
                }
            }
        }

        public override void OnInspectorGUI()
        {
            AudioPropagateNodeGroup pGroup = (AudioPropagateNodeGroup)target;

            PropagateLogoGUI.DrawLogo();

            pGroup.maxConnectDistance = EditorGUILayout.FloatField("Max Connect Distance", pGroup.maxConnectDistance);

            if (GUILayout.Button("Add Node"))
            {
                var view = SceneView.currentDrawingSceneView;
                Vector3 position = pGroup.transform.position;
                if (selectedNode != null)
                    position = selectedNode.position;
                if (view != null)
                {
                    Transform camT = view.camera.transform;
                    RaycastHit hit;
                    if (Physics.Raycast(camT.position, camT.forward, out hit, Mathf.Infinity))
                    {
                        position = hit.point + (hit.normal * 5f);
                    }
                    else
                    {
                        position = camT.position + (camT.forward * 5.0f);
                    }
                }
                selectedNode = pGroup.AddNode(position);
                SceneView.RepaintAll();
            }
            if (GUILayout.Button("Delete Node") && selectedNode != null)
            {
                pGroup.DeleteNode(selectedNode);
                selectedNode = null;
                SceneView.RepaintAll();
            }

            if (selectedNode != null)
            {
                string buttonText = isLinking ? "Cancel Link Node" : "Link Node";
                if (GUILayout.Button(buttonText))
                {
                    isLinking = !isLinking;
                }
            }
            else
            {
                isLinking = false;
            }
        }

        void UpdateNode(Node node)
        {
            Vector3 lastPosition = node.position;
            node.position = Handles.PositionHandle(node.position, Quaternion.identity);
            if (lastPosition != node.position)
            {
                ((AudioPropagateNodeGroup)target).RefreshNodeConnections(node);
            }
        }

        void DrawNode(Node node, int nodeIndex)
        {
            if (Handles.Button(node.position, Quaternion.identity, .3f, .4f, Handles.SphereCap))
            {
                if (isLinking && selectedNode != null)
                {
                    AudioPropagateNodeGroup pGroup = (AudioPropagateNodeGroup)target;
                    pGroup.ConnectNodes(selectedNode, node);
                    isLinking = false;
                }
                selectedNode = node;
            }

            //Handles.Label(node.position, node.id.ToString());

            for (int i = 0; i < node.connections.Count; ++i)
            {
                var otherNode = node.connections[i];

                Handles.DrawLine(node.position, otherNode.position);
            }
        }
    }

}