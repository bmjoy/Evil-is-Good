using UnityEngine;
using System.Collections;

namespace Locogame.Propagate
{
    public class Alarm : MonoBehaviour
    {
        public float rotateSpeed = 2f;

        void Update()
        {
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0, Space.Self);
        }
    }
}


