using UnityEngine;
using System.Collections;

namespace Locogame.Propagate
{
    public class PropagateDemoMotor : MonoBehaviour
    {
        public float moveSpeed = 1.0f;
        CharacterController _controller;

        // Use this for initialization
        void Start()
        {
            _controller = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            Vector3 motion = Physics.gravity;
            Vector3 moveDirection = transform.rotation * (inputDirection.normalized * moveSpeed);
            motion += moveDirection;
            _controller.Move(motion * Time.fixedDeltaTime);
        }
    }
}