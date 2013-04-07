using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GBLProjectAssemblies 
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerBinding : MonoBehaviour
    {
        public float speed = 3.0F;
        public float rotateSpeed = 3.0F;
        void Update()
        {
            CharacterController controller = GetComponent<CharacterController>();
            var waypoint = GameController.Instance.NextWayPoint;
            if (waypoint == null)
                return;
            //Debug.Log("Distance: " + (transform.position - waypoint.transform.position));
            transform.LookAt(waypoint.transform.position);

            //transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
            Vector3 moveDirection = transform.TransformDirection(Vector3.forward);
            moveDirection += GameController.Instance.PlayerRelativePosition;


            controller.Move(moveDirection * speed*Time.deltaTime);
            
            Debug.Log("IsGrounded:" + controller.isGrounded);
            
        }
    }
}
