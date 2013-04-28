using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GBLProjectAssemblies
{
    public class GuiTextureBinding : MonoBehaviour
    {

        public int TextureBindingNr;

        public void Start()
        {
            GameController.Instance.RegisterGuiTextureBinding(this);

        }
    }
}
