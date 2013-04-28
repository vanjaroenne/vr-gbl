using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GBLProjectAssemblies
{
    public class PlayerBinding : MonoBehaviour
    {
        public float speed = 3.0F;
        public float rotateSpeed = 3.0F;
        public float sidewayMoveSpeed = 3.0F;
        public float userMovementFactor = 4.0f;
        public float smallJumpThreshold = 0.2f;
        public float smallJumpHeight = 3f;
        public float highJumpThreshold = 0.4f;
        public float highJumpHeight = 10f;
        public float variationTolerance = 0.02f;

        private float userHeightSum = 0f;
        private long numberOfFrames = 0;
        private float maxHeight = float.MinValue;

        void Start()
        {
            GameController.Instance.Player = this.gameObject;
            userHeightSum = 0;
            numberOfFrames = 0;
        }

        void FixedUpdate()
        {
            
            
            transform.LookAt(GameController.Instance.Goal.transform);

            //transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
            Vector3 moveDirection = transform.TransformDirection(Vector3.forward);
            moveDirection.y = 0;
            //Debug.Log("Move dir:" + moveDirection);
            //var playerPos = GameController.Instance.PlayerRelativePosition;
            //moveDirection += new Vector3(0,0,Input.GetAxis("Horizontal"));
            // HARDCODED WAY: moveDirection += new Vector3(0, 0, CalculateLeftRightMovement());
            moveDirection += CalculateLeftRightMovementVector();
            //controller.Move(moveDirection * speed*Time.deltaTime);

            transform.position += moveDirection * speed;

            var body = GetComponent<Rigidbody>();


            if (IsGrounded())
            {
                var jumpHeight = CalculateJumpHeight();
                if (jumpHeight > 0.0000001)
                {
                    body.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
                    numberOfFrames = 0;
                    userHeightSum = 0;

                }
            }

        }

        /** HARDCODED WAY
        float CalculateLeftRightMovement()
        {
            var usersZPos = GameController.Instance.UserRelativePosition.normalized.z * userMovementFactor;
            if (usersZPos < -1)
                usersZPos = -1;
            if (usersZPos > 1)
                usersZPos = 1;

           // Debug.Log("User: " + GameController.Instance.UserRelativePosition.normalized);
            var usersPercentwiseDistanceToRight = (usersZPos + 1) / 2;
            var rightSide = GetRightBoundary().z;
            var leftSide = GetLeftBoundary().z;
            var difference = rightSide - leftSide;
            var playersDistanceToRight = transform.position.z - leftSide;
            //Debug.Log("Right side: " + rightSide + " Left side: "+ leftSide + " players pos: " + transform.position.z);
            var playersPercentwiseDistToRight = playersDistanceToRight / difference;
           // Debug.Log("User: " + usersPercentwiseDistanceToRight + " player: " + playersPercentwiseDistToRight);

            var result = (usersPercentwiseDistanceToRight - playersPercentwiseDistToRight) * sidewayMoveSpeed;
            //Debug.Log("UsersZ: " + usersZPos + " userPerc: " + usersPercentwiseDistanceToRight + " playersPerc: " + playersPercentwiseDistToRight + " result: " + result);
            return result;
        }*/

        Vector3 CalculateLeftRightMovementVector()
        {
            var usersZPos = GameController.Instance.UserRelativePosition.normalized.z * userMovementFactor;
            if (usersZPos < -1)
                usersZPos = -1;
            if (usersZPos > 1)
                usersZPos = 1;

            // Debug.Log("User: " + GameController.Instance.UserRelativePosition.normalized);
            var usersPercentwiseDistanceToRight = (usersZPos + 1) / 2;
            var rightSide = GetRightBoundary();
            var leftSide = GetLeftBoundary();
            var distance = Vector3.Distance(rightSide,leftSide);
            var playersDistanceToRight = Vector3.Distance(transform.position,leftSide);
            //Debug.Log("Right side: " + rightSide + " Left side: "+ leftSide + " players pos: " + transform.position.z);
            var playersPercentwiseDistToRight = playersDistanceToRight / distance;
            // Debug.Log("User: " + usersPercentwiseDistanceToRight + " player: " + playersPercentwiseDistToRight);

            var percentCorrection = (usersPercentwiseDistanceToRight - playersPercentwiseDistToRight) * sidewayMoveSpeed;
            //Debug.Log("UsersZ: " + usersZPos + " userPerc: " + usersPercentwiseDistanceToRight + " playersPerc: " + playersPercentwiseDistToRight + " result: " + result);
            
            return (rightSide - leftSide)*percentCorrection;
        }


        private float CalculateJumpHeight()
        {
            var average = (numberOfFrames == 0 ? 0 : userHeightSum / numberOfFrames);
            var userHeight = (1 + GameController.Instance.UserRelativePosition.normalized.y) / 2;
            if (userHeight < average * (1 + variationTolerance) || numberOfFrames < 10)
            {
                userHeightSum += userHeight;
                numberOfFrames++;
                return 0;
            }
            //Debug.Log("Number of frames: " + numberOfFrames + " Jump percent: " + (((userHeight / average) - 1) * 100) + " %");

            if (userHeight > maxHeight)
            {
                maxHeight = userHeight;
                return 0;
            }
            if (maxHeight > average * (1 + highJumpThreshold))
            {
                Debug.Log("High:" + ((maxHeight / average - 1 ) * 100) + "%");
                return highJumpHeight;
            }
            if (maxHeight > average * (1 + smallJumpThreshold))
            {
                Debug.Log("Low:" + ((maxHeight / average - 1) * 100) + "%");
                return smallJumpHeight;
            }
            return 0;

        }

        private bool isGrounded = false;
        private bool IsGrounded()
        {
            return isGrounded;
        }

        void OnCollisionEnter(Collision coll)
        {
            if (coll.gameObject.name == "Plane")
                isGrounded = true;
        }

        void OnCollisionExit(Collision coll)
        {
            if (coll.gameObject.name == "Plane")
                isGrounded = false;
        }



        Vector3 GetSideBoundary(String side)
        {
            GameObject boundary = GameObject.Find(side);
            return boundary.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
        }

        Vector3 GetRightBoundary()
        {
            return GetSideBoundary("Right");
        }

        Vector3 GetLeftBoundary()
        {
            return GetSideBoundary("Left");
        }

    /*    void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Pickup")
            {
               Mixed.SP.foundFish();
                Destroy(other.gameObject);
            }
            else
            {
            }
        } */

    }
}
