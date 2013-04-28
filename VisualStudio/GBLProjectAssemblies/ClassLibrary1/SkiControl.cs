using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GBLProjectAssemblies
{
    public class SkiControl : MonoBehaviour
    {

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Pickup")
            {
                MainGameManager.SP.FoundFish();
                Destroy(other.gameObject);
            }
            else
            {
                //Other collider.. See other.tag and other.name
            }
        }
    }
}
