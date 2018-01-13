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
	[RequireComponent(typeof(AudioSource))]
	[RequireComponent(typeof(AudioLowPassFilter))]
	public class PropagateSound : MonoBehaviour
	{
		struct TransientNode
		{
			public float distanceToSource;
			public float curvatureToSource;
			public Quaternion direction;
			public Node parent;
		}

        [Range(0f, 1f)]
        public float masterVolume;
		public PropagateProfile profile;
		public float updateRate = 0.2f;
		public bool drawDebugPath;

		AudioLowPassFilter lowpass;
		new Transform transform;
		AudioSource audioSource;

		Dictionary<Node, TransientNode> _transientNodes;
		Transform _sourceTransform;
		Vector3 _perceivedPosition;
		Vector3 _lastSourcePosition;
		Vector3 _lastListenerPosition;
		float _cutoffFrequency;
		float _volume;
		Transform _listenerTransform;
		Node[] _allNodes;
		Node _nearestNodeToListener;
		float _updateTimer = 0;
        Collider[] _sourceColliders;
        bool _los;
		
		void Awake()
		{
			transform = GetComponent<Transform>();
			lowpass = GetComponent<AudioLowPassFilter>();
			audioSource = GetComponent<AudioSource>();
			_transientNodes = new Dictionary<Node, TransientNode>();
			_listenerTransform = FindObjectOfType<AudioListener>().transform;
			_allNodes = AudioPropagateNodeGroup.GetAllNodes();
			audioSource.volume = 0;
		}

		void Start()
		{
			_sourceTransform = transform.parent;
			if (_sourceTransform == null)
			{
                Debug.LogError(string.Format("[Propagate] PropagateSound needs to be a child of another GameObject. ({0})", gameObject.name));
				this.enabled = false;
				return;
			}
			transform.SetParent(null, worldPositionStays: true);
			_perceivedPosition = transform.position;
            _sourceColliders = _sourceTransform.GetComponents<Collider>();
			Propagate();
		}

		void Update()
		{
			// update transform.
			float transformSlerpT = profile.percievedPositionResponsiveness * Time.deltaTime;
			transform.position = Vector3.Lerp(transform.position, _perceivedPosition, transformSlerpT);
			// update lowpass filter.
			float lowpassLerpT = profile.frequencySettings.responsiveness * Time.deltaTime;
			lowpass.cutoffFrequency = Mathf.Lerp(lowpass.cutoffFrequency, _cutoffFrequency, lowpassLerpT);
			// update volume.
			float targetVolume = _volume * masterVolume;
			float volumeLerpT = profile.volumeSettings.responsiveness * Time.deltaTime;
			audioSource.volume = Mathf.Lerp(audioSource.volume, targetVolume, volumeLerpT);

			// Check if the listener has LOS to the audio source.
            foreach (Collider col in _sourceColliders)
                col.enabled = false;
            _los = !Physics.Linecast(_listenerTransform.position, _sourceTransform.position);
            foreach (Collider col in _sourceColliders)
                col.enabled = true;

            if (_los)
			{
				_perceivedPosition = _sourceTransform.position;
				_cutoffFrequency = profile.frequencySettings.maxCutoff;
                _volume = 1.0f;
			}
			else
			{
				float deltaSourcePosition = (_sourceTransform.position - _lastSourcePosition).sqrMagnitude;
				float deltaListenerPosition = (_listenerTransform.position - _lastListenerPosition).sqrMagnitude;
				_lastSourcePosition = _sourceTransform.position;
				_lastListenerPosition = _listenerTransform.position;
				_updateTimer += Time.deltaTime;

				if (deltaSourcePosition > 0 && _updateTimer >= updateRate)
				{
					Propagate();
					_updateTimer = 0;
				}

				UpdateAudioSettings();
			}

			// Draw path.
			if (drawDebugPath)
			{
                DrawDebugPath();
			}
		}

		void Propagate()
		{
			_transientNodes.Clear();
			var openNodes = new List<Node>();

			// Find nearest node.
            Node nearestNode = NearestNodeInLOS(_sourceTransform.position);

            if (nearestNode == null)
            {
                Debug.LogError("[Propagate] Could not find PropagateNode in line of sight to audio source. Falling back to standard Audio Source.");
                audioSource.volume = 1.0f;
                this.enabled = false;
                return;
            }

			Node currentNode = nearestNode;
			_transientNodes[currentNode] = new TransientNode(){parent = null};

			while (currentNode != null)
			{
				TransientNode tNode = _transientNodes[currentNode];
				if (tNode.parent != null)
				{
					TransientNode parentTNode = _transientNodes[tNode.parent];
					tNode.distanceToSource = parentTNode.distanceToSource + Vector3.Distance(tNode.parent.position, currentNode.position);
					tNode.direction = Quaternion.Euler((currentNode.position - tNode.parent.position).normalized);
					tNode.curvatureToSource = parentTNode.curvatureToSource + Quaternion.Angle(parentTNode.direction, tNode.direction);
				}
				else
				{
					tNode.distanceToSource = Vector3.Distance(currentNode.position, _sourceTransform.position);
					tNode.curvatureToSource = 0;
					tNode.direction = Quaternion.identity;
					tNode.parent = null;
				}
				_transientNodes[currentNode] = tNode;

				foreach (var connectedNode in currentNode.connections)
				{
					if (!_transientNodes.ContainsKey(connectedNode) && !openNodes.Contains(connectedNode))
					{
						openNodes.Add(connectedNode);
						_transientNodes[connectedNode] = new TransientNode(){
							parent = currentNode
						};
					}
				}

				if (openNodes.Count > 0)
				{
					currentNode = openNodes[0];
					openNodes.RemoveAt(0);
				}
				else
				{
					currentNode = null;
				}
			}
		}

		void UpdateAudioSettings()
		{
			// Find nearest node to listener.
            _nearestNodeToListener = NearestNodeInLOS(_listenerTransform.position);
            if (_nearestNodeToListener == null)
            {
                Debug.Log("[Propagate] No PropagateNodes in line of sight of listener.");
                _nearestNodeToListener = NearestNode(_listenerTransform.position);
                if (_nearestNodeToListener == null)
                {
                    Debug.LogError("[Propagate] No PropagateNodes near listener. Falling back to standard audio source.");
                    audioSource.volume = 1.0f;
                    this.enabled = false;
                    return;
                }
            }
			
			// Find the last visible node along the path.
			Node currentNode = _nearestNodeToListener;
			while(currentNode != null && _transientNodes.ContainsKey(currentNode))
			{
				TransientNode tNode = _transientNodes[currentNode];
				if (tNode.parent != null)
				{
                    if (CheckLOSToNode(tNode.parent, _listenerTransform.position))
                    {
                        _nearestNodeToListener = tNode.parent;
                    }
                    else
                    {
                        break;
                    }
				}
				currentNode = tNode.parent;
			}

            if (drawDebugPath)
            {
                //Debug.DrawLine(_listenerTransform.position, _nearestNodeToListener.position, Color.red);
            }

            TransientNode nearestTNodeToListener;
            bool didGetNearestNode = _transientNodes.TryGetValue(_nearestNodeToListener, out nearestTNodeToListener);
            if (!didGetNearestNode)
            {
                Debug.LogError("[Propagate] No node near to listener.");
            }

            // Add the distance from last node to listener.
            float nodeListenerDist = Vector3.Distance(_nearestNodeToListener.position, _listenerTransform.position);
			float totalDistance = nearestTNodeToListener.distanceToSource + nodeListenerDist;
			
			// Move the audio source.
            _perceivedPosition = Vector3.Lerp(_sourceTransform.position, _nearestNodeToListener.position, profile.percievedPositionEffect);
			Vector3 listenerNodeDirection = (_perceivedPosition - _listenerTransform.position).normalized;
			_perceivedPosition = _listenerTransform.position + (listenerNodeDirection * totalDistance);
			
			// Calculate the lowpass.
			float maxAngle = profile.frequencySettings.maxAngle;
			float normalizedAngle = RemapFloat(nearestTNodeToListener.curvatureToSource * Mathf.Rad2Deg, 0f, maxAngle, 0f, 1f);
			float normalizedFrequency = profile.frequencySettings.curve.Evaluate(normalizedAngle);
			_cutoffFrequency = RemapFloat(normalizedFrequency,
			                          0f, 1f, 
			                          profile.frequencySettings.minCutoff, 
			                          profile.frequencySettings.maxCutoff);


			// Calculate volume reduction due to blocked LOS.
            _volume = 1.0f;
			RaycastHit hitInfo;
			if (Physics.Linecast(_listenerTransform.position, _nearestNodeToListener.position, out hitInfo) && !hitInfo.collider.CompareTag("Player"))
			{
                AudioOccluder occluder = hitInfo.collider.GetComponent<AudioOccluder>();
                if (occluder != null)
                {
                    _volume *= 1f - occluder.volumeReduction;
                    _cutoffFrequency *= 1f - occluder.frequencyReduction;
                }
                else
                {
                    _volume *= 1f - profile.occlusionSettings.volumeReduction;
                    _cutoffFrequency *= 1f - profile.occlusionSettings.frequencyReduction;
                }
			}

			currentNode = _nearestNodeToListener;
			while(currentNode != null && _transientNodes.ContainsKey(currentNode))
			{
				TransientNode tNode = _transientNodes[currentNode];
				if (tNode.parent != null && Physics.Linecast(currentNode.position, tNode.parent.position, out hitInfo))
				{
                    AudioOccluder occluder = hitInfo.collider.GetComponent<AudioOccluder>();
                    if (occluder != null)
                    {
                        _volume *= 1f - occluder.volumeReduction;
                        _cutoffFrequency *= 1f - occluder.frequencyReduction;
                    }
                    else
                    {
                        _volume *= 1f - profile.occlusionSettings.volumeReduction;
                        _cutoffFrequency *= 1f - profile.occlusionSettings.frequencyReduction;
                    }
				}
				currentNode = tNode.parent;
			}
		}

        bool CheckLOSToNode(Node node, Vector3 origin)
        {
            Vector3 direction = (node.position - origin);
            float distance = direction.magnitude;
            direction = direction.normalized;

            RaycastHit[] hits = Physics.RaycastAll(origin, direction, distance);
            int hitCount = hits.Length;
            for (int hitIndex = 0; hitIndex < hits.Length; hitIndex++)
            {
                RaycastHit hit = hits[hitIndex];
                if (hit.collider.isTrigger)
                {
                    hitCount--;
                }
            }

            return (hitCount == 0);
        }

		Node NearestNodeInLOS(Vector3 position)
		{
            System.Array.Sort(_allNodes, (n1, n2) => {
                float n1Dist = (n1.position - position).sqrMagnitude;
                float n2Dist = (n2.position - position).sqrMagnitude;
                return n1Dist.CompareTo(n2Dist);
            });

            for (int i = 0; i < _allNodes.Length; ++i)
            {
                Node node = _allNodes[i];
                if (CheckLOSToNode(node, position))
                {
                    return node;
                }
            }
    
            // No nodes found if program reaches this point.
            Debug.LogError(string.Format("[Propagate] No nodes found near position {0} with line of sight.", position.ToString()));
            return null;
		}

        Node NearestNode(Vector3 position)
        {
            Node nearestNode = _allNodes[0];
            float maxDist = Mathf.Infinity;
            for (int i = 0; i < _allNodes.Length; i++)
            {
                Node node = _allNodes[i];
                float dist = (node.position - position).sqrMagnitude;
                if (dist < maxDist)
                {
                    maxDist = dist;
                    nearestNode = node;
                }
            }

            return nearestNode;
        }

		float RemapFloat(float value, float from1, float to1, float from2, float to2) {
			return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
		}

        void DrawDebugPath()
        {
            if (_los)
            {
                Debug.DrawLine(_sourceTransform.position, _listenerTransform.position, Color.red);
            }
            else
            {
                Color lineColor = Color.red;
                if (_nearestNodeToListener != null)
                {
                    Debug.DrawLine(_listenerTransform.position, _nearestNodeToListener.position, lineColor);
                }
                Node currentNode = _nearestNodeToListener;
                while (currentNode != null && _transientNodes.ContainsKey(currentNode))
                {
                    TransientNode tNode = _transientNodes[currentNode];
                    Node parentNode = tNode.parent;
                    if (parentNode != null)
                    {
                        Debug.DrawLine(currentNode.position, parentNode.position, lineColor);
                    }
                    else
                    {
                        Debug.DrawLine(currentNode.position, _sourceTransform.position, lineColor);
                    }
                    currentNode = parentNode;
                }
            }
        }
        
	}

}