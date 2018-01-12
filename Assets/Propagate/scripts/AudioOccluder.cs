/* Copyright (C) Luke Perkin, locogame.co.uk - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * without the express permission from the author.
 * Written by Luke Perkin <lukeperkin@gmail.com>, January 2015
 */

using UnityEngine;
using System.Collections;

namespace Locogame.Propagate
{
	public class AudioOccluder : MonoBehaviour
	{
        [Tooltip("The amount this object reduces volume. 0.5 = 50% reduction, 1 = 100% reduction.")]
		[Range(0,1)]
		public float volumeReduction = 0.1f;

        [Tooltip("The amount this object reduces frequency responce. 0.5 = 50% reduction, 1 = 100% reduction")]
		[Range(0,1)]
		public float frequencyReduction = 0.1f;
	}
}