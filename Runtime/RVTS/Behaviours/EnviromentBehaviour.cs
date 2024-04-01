using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

namespace RVTS.Behaviours
{
    public static class EnviromentBehaviour
    {
        public static EventHandler OnStartGame;
        public static EventHandler OnPauseGame;
        public static EventHandler OnReloadGame;
        public static EventHandler OnEndGame;
        public static EventHandler<int> OnPhaseChange;
    }
}