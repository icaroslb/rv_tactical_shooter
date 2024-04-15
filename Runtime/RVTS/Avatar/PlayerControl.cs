using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
//using UnityEngine.InputSystem;

namespace RVTS.Player
{

    public class PlayerControl : MonoBehaviour
    {
        // Rays
        public XRRayInteractor leftRayUI;
        public XRRayInteractor rightRayUI;

        public void EnableUIInteractor(bool e)
        {
            leftRayUI.gameObject.SetActive(e);
            rightRayUI.gameObject.SetActive(e);
        }
    }
}
