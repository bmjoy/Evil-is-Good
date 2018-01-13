/* Copyright (C) Luke Perkin, locogame.co.uk - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * without the express permission from the author.
 * Written by Luke Perkin <lukeperkin@gmail.com>, January 2015
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Locogame.Propagate
{
	[ExecuteInEditMode]
	public class AudioPropagateNodeGroup : MonoBehaviour, ISerializationCallbackReceiver
	{
		private static Node[] _allNodes;
		public static Node[] AllNodes {
			get {
				if (_allNodes == null)
				{
					_allNodes = GetAllNodes();
				}
				return _allNodes;
			}
		}

		const int MAX_NODES = 2048;
        public float maxConnectDistance = 10.0f;

		public Node[] nodes = new Node[MAX_NODES];
		[HideInInspector]
		public int nodeCount;
		private int nextNodeID = 0;

		public static Node[] GetAllNodes()
		{
			AudioPropagateNodeGroup[] groups = FindObjectsOfType<AudioPropagateNodeGroup>();

			int totalNodes = 0;
			foreach (var group in groups)
			{
				totalNodes += group.nodeCount;
			}

			Node[] allNodes = new Node[totalNodes];
			int allNodesIndex = 0;
			foreach (var group in groups)
			{
				for (int i = 0; i < group.nodeCount; ++i)
				{
					allNodes[allNodesIndex++] = group.nodes[i];
				}
			}

			return allNodes;
		}

		public Node AddNode(Vector3 position)
		{
			Node node = new Node(){
				id = nextNodeID++,
				position = position,
				connections = new List<Node>()
			};

			ConnectToNearbyNodes(node);
			nodes[nodeCount++] = node;
			return node;
		}

		public void DeleteNode(Node node)
		{
			DisconnectAll(node);
			// remove node from nodes array.
			for (int i = 0; i < nodeCount; ++i)
			{
				if (nodes[i] == node)
				{
					nodes[i] = null;
					break;
				}
			}
			// collapse node array down to fill in null gaps
			for (int i = 0; i < nodeCount - 1; ++i)
			{
				if (nodes[i] == null)
				{
					nodes[i] = nodes[i+1];
					nodes[i+1] = null;
				}
			}

			nodeCount--;
		}

		void ConnectToNearbyNodes(Node node)
		{
			for (int i = 0; i < nodeCount; ++i)
			{
				Node otherNode = nodes[i];
				if (otherNode.id == node.id)
					continue;

				float distance = Vector3.Distance(node.position, otherNode.position);
				if (distance < maxConnectDistance)
				{
					bool didHit = Physics.Linecast(node.position, otherNode.position);
					if (!didHit)
						ConnectNodes(node, otherNode);
				}
			}
		}

		public void RefreshNodeConnections(Node node)
		{
			DisconnectAll(node);
			ConnectToNearbyNodes(node);
		}

		public Node FindNodeByID(int id)
		{
			for (int i = 0; i < nodeCount; ++i)
			{
				if (nodes[i].id == id)
					return nodes[i];
			}
			return null;
		}

		public void ConnectNodes(Node n1, Node n2)
		{
			if (n1.id == n2.id)
				return;

			if (!n1.connections.Contains(n2) && !n2.connections.Contains(n1))
            {
				n1.connections.Add(n2);
                n2.connections.Add(n1);
            }
		}

		void DisconnectNodes(Node n1, Node n2)
		{
			n1.connections.Remove(n2);
			n2.connections.Remove(n1);
		}

		void DisconnectAll(Node node)
		{
			if (node.connections.Count > 0)
			{
                int totalConnections = node.connections.Count;
                for (int i = totalConnections-1; i >= 0; i--)
				{
					Node otherNode = node.connections[i];
					DisconnectNodes(node, otherNode);
				}
			}
		}

		#region ISerializationCallbackReceiver implementation
		public void OnBeforeSerialize ()
		{
			for (int i = 0; i < nodeCount; ++i)
			{
				Node node = nodes[i];
				node.connectionIds = new List<int>();
				foreach (Node connectedNode in node.connections)
				{
					node.connectionIds.Add(connectedNode.id);
				}
			}
		}
		
		public void OnAfterDeserialize ()
		{
			for (int i = 0; i < nodeCount; ++i)
			{
				Node node = nodes[i];
				node.connections = new List<Node>();
				foreach (int connectedId in node.connectionIds)
				{
					Node connectedNode = FindNodeByID(connectedId);
					node.connections.Add(connectedNode);
				}

				if (node.id > nextNodeID)
					nextNodeID = node.id + 1;
			}
		}
		#endregion
	}

	[System.Serializable]
	public class Node : IEqualityComparer<Node>
	{
		public int id;
		
		public Vector3 position;
		
		[System.NonSerialized]
		public float deltaAngle;
		[System.NonSerialized]
		public float distanceAlongPath;
		[System.NonSerialized]
		public float distanceToTarget;
		[System.NonSerialized]
		public float distanceToParentNode;
		[System.NonSerialized]
		public float volumeReduction;
		[System.NonSerialized]
		public float frequencyReduction;
		[System.NonSerialized]
		public Node parent;
		[System.NonSerialized]
		public bool visited;
		[System.NonSerialized]
		public float cost;
		
		[System.NonSerialized]
		public List<Node> connections;
		
		[SerializeField]
		public List<int> connectionIds;

		public static int Sort(Node a, Node b)
		{
			return a.cost.CompareTo(b.cost);
		}

        public bool Equals(Node x, Node y)
        {
            return x.id == y.id;
        }

        public int GetHashCode(Node obj)
        {
            return obj.id.GetHashCode();
        }
    }
}