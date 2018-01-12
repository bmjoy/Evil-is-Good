/* Copyright (C) Luke Perkin, locogame.co.uk - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * without the express permission from the author.
 * Written by Luke Perkin <lukeperkin@gmail.com>, January 2015
 */

using UnityEngine;
using System.Collections;

namespace Locogame.Propagate
{
    [System.Serializable]
    public class VolumeSettings
    {
        [Tooltip("Larger values cause the attenuation to respond more rapidly to the listeners change in movement")]
        public float responsiveness = 1.0f;
        [Tooltip("Minimum volume in %")]
        [Range(0f, 1f)]
        public float minVolume = 0.01f;
        [Tooltip("Maximum volume in %")]
        [Range(0f, 1f)]
        public float maxVolume = 1f;

        //TODO: impliment lookaway volume modifier.
        //[Tooltip("Descreases the volume based on whether the listener is looking away from the audio source. 0.5 = 50% reduction")]
        //[Range(0f,1f)]
        //public float lookawayReduction = 0.1f;

        //[Tooltip("The volume falloff curve with respect to listener distance")]
        //public AnimationCurve curve = AnimationCurve.Linear(0, 1, 1, 0);
    }

    [System.Serializable]
    public class FrequencySettings
    {
        [Tooltip("Larger values cause the frequency cutoff to respond more rapidly to the listeners change in movement")]
        public float responsiveness = 1.0f;
        [Tooltip("The minimum frequency that audio will be cutoff to")]
        [Range(0, 20000)]
        public float minCutoff;
        [Tooltip("The maximum frequency that audio will be cutoff to")]
        [Range(0, 20000)]
        public float maxCutoff = 20000.0f;
        [Tooltip("The maximum change in angle the propogating audio has to take before it reaches minimum frequency responce")]
        public float maxAngle = 180.0f;

        //TODO: implement lookaway freq. cutoff modifier.
        //[Tooltip("Descreases the frequency responce based on wether the listener is looking away from the audio source")]
        //[Range(0,1)]
        //public float lookawayReduction = 0.1f;

        [Tooltip("The frequency responce falloff curve with respect to propagation curvature")]
        public AnimationCurve curve = AnimationCurve.Linear(0, 1, 1, 0);
    }

    [System.Serializable]
    public class OcclusionSettings
    {
        [Tooltip("The amount a solid object reduces volume. 0.5 = 50% reduction, 1 = 100% reduction.")]
        [Range(0f, 1f)]
        public float volumeReduction;
        [Tooltip("The amount a solid object reduces frequency responce. 0.5 = 50% reduction, 1 = 100% reduction")]
        [Range(0f, 1f)]
        public float frequencyReduction;
    }

    public class PropagateProfile : ScriptableObject
    {
        public VolumeSettings volumeSettings;
        public FrequencySettings frequencySettings;
        public OcclusionSettings occlusionSettings;

        [Tooltip("How fast the audio source moves towards the perceived audio source position")]
        public float percievedPositionResponsiveness = 1.0f;

        [Tooltip("How much the percieved position effects the actual position of the audio. 0 = 0%, 1 = 100%")]
        [Range(0.0f, 1.0f)]
        public float percievedPositionEffect = 0.5f;
    }

}