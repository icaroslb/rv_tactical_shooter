using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using RVTS.Objects;

namespace RVTS.Behaviours
{
    public class BulletBehaviour : IObjectBehaviour
    {
        public static GameObject CreateObject(Transform parent, Transform target, GameObject bullet_prefab)
        {
            Vector3 look_at = (target.position - parent.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(look_at, Vector3.up);

            GameObject new_bullet_gameobject = Object.Instantiate(bullet_prefab, parent.position, rotation);
            Bullet new_bullet = new_bullet_gameobject.GetComponent<Bullet>();

            new_bullet_gameobject.transform.localScale = bullet_size_;

            new_bullet_gameobject.GetComponent<Rigidbody>().AddForce(look_at * new_bullet.speed);

            return new_bullet_gameobject;
        }

        private static Vector3 bullet_size_ = new Vector3(2f, 2f, 2f);
    }
}