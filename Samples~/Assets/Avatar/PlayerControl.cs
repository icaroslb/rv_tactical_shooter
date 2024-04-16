using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using UnityEngine.InputSystem;

namespace RVTS.Player
{

    public class PlayerControl : MonoBehaviour
    {
        // Rays
        public UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor leftRayUI;
        public UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor rightRayUI;

        public void EnableUIInteractor(bool e)
        {
            leftRayUI.gameObject.SetActive(e);
            rightRayUI.gameObject.SetActive(e);
        }
    }
}
