using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GBLProjectAssemblies
{
    public class CheckpointBinding : MonoBehaviour
    {
        private bool hasBeenHit = false;

        public void Start() {
            hasBeenHit = false;
        }

        public void OnTriggerEnter(Collider other)
        {
            Debug.Log("Got hit");
            if (hasBeenHit)
                return;
            GameController.Instance.RemoveCurrentWayPoint();
            hasBeenHit = true;
            this.gameObject.SetActive(false);
        }


    }
}
