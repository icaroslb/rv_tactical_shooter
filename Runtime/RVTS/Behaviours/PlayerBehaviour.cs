using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RVTS.Behaviours
{
    public static class PlayerBehaviour
    {
        public class OnCollisionArgs
        {
            public GameObject object_collided_;
            public Type object_type_;
        }

        public static EventHandler<OnCollisionArgs> OnCollision;
    }
}
