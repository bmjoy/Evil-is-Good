using UnityEngine;
using System.Collections;

namespace Locogame.Propagate
{
    public class PropagateDoorTrigger : MonoBehaviour
    {
        public Animator animator;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                animator.SetTrigger("open");
                GetComponent<AudioSource>().Play();
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                animator.SetTrigger("close");
                GetComponent<AudioSource>().Play();
            }
        }
    }
}