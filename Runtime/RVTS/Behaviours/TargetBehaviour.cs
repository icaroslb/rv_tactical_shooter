using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RVTS.Behaviours
{
    public static class TargetBehaviour
    {
        public class TargetEventArgs
        {
            public int id_;
            public Transform transform_;
        }

        public static EventHandler<TargetEventArgs> OnStartEvent;
        public static EventHandler<TargetEventArgs> OnEvent;
        public static EventHandler<TargetEventArgs> OnEndEvent;
    }
}