using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RVTS.Behaviours
{
    public static class WeaponBehaviour
    {
        public class OnShootArgs
        {
            public bool was_hit_;
            public GameObject object_hitted_;
            public LayerMask layer_mask_;
            public Vector3 hit_position_;
            public int munition_;
        }

        public class WeaponInitializeArgs
        {
            public int munition_;
        }

        public static EventHandler<WeaponInitializeArgs> OnInitialize;
        public static EventHandler<OnShootArgs> OnShoot;
        public static EventHandler<int> OnReloadMunition;
    }
}