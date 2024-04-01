using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RVTS.Behaviours
{
    public interface IObjectBehaviour
    {
        public static GameObject CreateObject(Transform parent, Transform target, GameObject bullet_prefab) => throw new NotImplementedException();
    }
}